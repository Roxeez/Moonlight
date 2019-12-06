﻿using System;
 using System.Collections.Concurrent;
 using System.Collections.Generic;
 using System.Diagnostics;
 using System.Diagnostics.Eventing.Reader;
 using System.Runtime.InteropServices;
 using System.Threading;
 using System.Threading.Tasks;
 using NtCore.API.Client;
 using NtCore.API.Enums;
 using NtCore.API.Game.Entities;
 using NtCore.Game.Entities;
 using NtCore.Import;

 namespace NtCore.Clients
{
    public sealed class LocalClient : IClient
    {
        public event Action<string> PacketSend;
        public event Action<string> PacketReceived;

        private readonly ConcurrentQueue<string> _sendQueue = new ConcurrentQueue<string>();
        private readonly ConcurrentQueue<string> _recvQueue = new ConcurrentQueue<string>();
        
        /// <summary>
        /// Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;
        private readonly NtNative.PacketCallback _recvCallback;

        public ICharacter Character { get; }

        private readonly Thread _thread;
        private bool _dispose;
        
        public LocalClient(ProcessModule mainModule)
        {
            Character = new Character(this);

            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;
            
            NtNative.SetSendCallback(_sendCallback);
            NtNative.SetRecvCallback(_recvCallback);
            
            NtNative.Setup((uint)mainModule.BaseAddress, (uint)mainModule.ModuleMemorySize);

            _thread = new Thread(Loop);
            _thread.Start();
        }

        private void Loop()
        {
            while (!_dispose)
            {
                if (_sendQueue.TryDequeue(out string sendPacket))
                {
                    NtNative.SendPacket(sendPacket);
                }
                
                if (_recvQueue.TryDequeue(out string receivePacket))
                {
                    NtNative.RecvPacket(receivePacket);
                }

                Thread.Sleep(10);
            }
        }

        private void OnPacketSend(string packet) => PacketSend?.Invoke(packet);
        private void OnPacketReceived(string packet) => PacketReceived?.Invoke(packet);

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
    }
}