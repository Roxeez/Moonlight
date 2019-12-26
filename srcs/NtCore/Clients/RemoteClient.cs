using System;
using System.Threading.Tasks;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;

namespace NtCore.Clients
{
    public class RemoteClient : IClient
    {
        private readonly NetworkClient _networkClient;

        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;
        
        public Guid Id { get; }
        public ICharacter Character { get; }
        public ClientType Type { get; }

        public RemoteClient(NetworkClient networkClient)
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            Type = ClientType.REMOTE;

            _networkClient = networkClient;

            _networkClient.PacketReceived += packet => ReceivePacket(packet);
        }

        public async Task SendPacket(string packet)
        {
            await _networkClient.SendPacket(packet);
            PacketSend?.Invoke(packet);
        }
        
        public Task ReceivePacket(string packet)
        {
            PacketReceived?.Invoke(packet);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _networkClient.Dispose();
        }

        public bool Equals(IClient other) => other != null && other.Id == Id;
    }
}