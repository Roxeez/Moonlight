﻿using System;
using System.Diagnostics;
 using NtCore.Game.Entities;
 using NtCore.Game.Entities.Impl;
 using NtCore.Import;

 namespace NtCore.Core.Impl
{
    internal sealed class Client : IClient
    {
        public event Action<string> PacketSend;
        public event Action<string> PacketReceived;

        /// <summary>
        /// Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;
        private readonly NtNative.PacketCallback _recvCallback;
        
        public ICharacter Character { get; } = new Character();
        
        public Client(ProcessModule mainModule)
        {
            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;
            
            /*
            NtNative.SetSendCallback(_sendCallback);
            NtNative.SetRecvCallback(_recvCallback);
            
            NtNative.Setup((uint)mainModule.BaseAddress, (uint)mainModule.ModuleMemorySize);
            */
        }

        private void OnPacketSend(string packet) => PacketSend?.Invoke(packet);
        private void OnPacketReceived(string packet) => PacketReceived?.Invoke(packet);

        public void SendPacket(string packet)
        {
            OnPacketSend(packet);
            // NtNative.SendPacket(packet);
        }

        public void ReceivePacket(string packet)
        {
            OnPacketReceived(packet);
            // NtNative.RecvPacket(packet);
        }
    }
}