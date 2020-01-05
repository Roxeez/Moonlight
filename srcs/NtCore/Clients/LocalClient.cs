using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.Import;

namespace NtCore.Clients
{
    public sealed class LocalClient : IClient
    {
        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;
        private readonly NtNative.PacketCallback _recvCallback;
        
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
        }

        public Guid Id { get; }
        public Character Character { get; }
        public ClientType Type { get; }

        public Task SendPacket(string packet)
        {
            NtNative.SendPacket(packet);
            return Task.CompletedTask;
        }

        public Task ReceivePacket(string packet)
        {
            NtNative.RecvPacket(packet);
            return Task.CompletedTask;
        }

        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;

        public bool Equals(IClient other) => other != null && other.Id.Equals(Id);

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

        public void Dispose()
        {
            NtNative.Clean();
        }
    }
}