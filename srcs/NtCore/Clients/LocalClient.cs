using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using NtCore.API.Clients;
using NtCore.API.Game.Entities;
using NtCore.Game.Entities;
using NtCore.Import;

namespace NtCore.Clients
{
    public sealed class LocalClient : IClient
    {
        private readonly NtNative.PacketCallback _recvCallback;
        private readonly ConcurrentQueue<string> _recvQueue = new ConcurrentQueue<string>();

        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;

        private readonly ConcurrentQueue<string> _sendQueue = new ConcurrentQueue<string>();

        private readonly Thread _thread;
        private bool _dispose;

        public LocalClient(ProcessModule mainModule)
        {
            Character = new Character(this);

            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            NtNative.SetSendCallback(_sendCallback);
            NtNative.SetRecvCallback(_recvCallback);

            NtNative.Setup((uint) mainModule.BaseAddress, (uint) mainModule.ModuleMemorySize);

            _thread = new Thread(Loop);
            _thread.Start();
        }

        public ICharacter Character { get; }

        public void SendPacket(string packet)
        {
            _sendQueue.Enqueue(packet);
        }

        public void ReceivePacket(string packet)
        {
            _recvQueue.Enqueue(packet);
        }

        public void Dispose()
        {
            _dispose = true;
            _thread.Join();
        }

        public event Action<string> PacketSend;
        public event Action<string> PacketReceived;

        private void Loop()
        {
            while (!_dispose)
            {
                if (_sendQueue.TryDequeue(out var sendPacket)) NtNative.SendPacket(sendPacket);

                if (_recvQueue.TryDequeue(out var receivePacket)) NtNative.RecvPacket(receivePacket);

                Thread.Sleep(1);
            }
        }

        private void OnPacketSend(string packet)
        {
            PacketSend?.Invoke(packet);
        }

        private void OnPacketReceived(string packet)
        {
            PacketReceived?.Invoke(packet);
        }
    }
}