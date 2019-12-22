using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Import;

namespace NtCore.Clients
{
    public sealed class LocalClient : IClient
    {
        private readonly ConcurrentQueue<string> _recvQueue = new ConcurrentQueue<string>();

        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;
        private readonly NtNative.PacketCallback _recvCallback;

        private readonly ConcurrentQueue<string> _sendQueue = new ConcurrentQueue<string>();

        private readonly Thread _thread;
        private bool _dispose;

        public LocalClient()
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            Type = ClientType.LOCAL;

            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            NtNative.Initialize();

            NtNative.SetSendCallback(_sendCallback);
            NtNative.SetRecvCallback(_recvCallback);

            _thread = new Thread(Loop);
            _thread.Start();
        }

        public Guid Id { get; }
        public ICharacter Character { get; }
        public ClientType Type { get; }

        public Task SendPacket(string packet)
        {
            _sendQueue.Enqueue(packet);
            return Task.CompletedTask;
        }

        public Task ReceivePacket(string packet)
        {
            _recvQueue.Enqueue(packet);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dispose = true;
            _thread.Join();
        }

        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;

        private void Loop()
        {
            while (!_dispose)
            {
                while (_sendQueue.TryDequeue(out string sendPacket))
                {
                    NtNative.SendPacket(sendPacket);
                }

                while (_recvQueue.TryDequeue(out string receivePacket))
                {
                    NtNative.RecvPacket(receivePacket);
                }

                Thread.Sleep(10);
            }
        }

        private bool OnPacketSend(string packet)
        {
            if (PacketSend == null)
            {
                return true;
            }
            return PacketSend.Invoke(packet);
        }

        private bool OnPacketReceived(string packet)
        {
            if (PacketReceived == null)
            {
                return true;
            }
            return PacketReceived.Invoke(packet);
        }

        public bool Equals(IClient other) => other != null && other.Id.Equals(Id);
    }
}