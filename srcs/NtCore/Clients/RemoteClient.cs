using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using NtCore.Clients.Remote;
using NtCore.Cryptography;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;

namespace NtCore.Clients
{
    public class RemoteClient : IClient
    {
        public Guid Id { get; }
        public ICharacter Character { get; }
        public ClientType Type { get; }

        private readonly INetworkClient _networkClient;
        
        public RemoteClient(INetworkClient networkClient)
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            Type = ClientType.REMOTE;

            _networkClient = networkClient;

            _networkClient.PacketReceived += packet => PacketReceived?.Invoke(packet);
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

        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;
        
        public void Dispose()
        {
            _networkClient.Dispose();
        }

        public bool Equals(IClient other) => other != null && other.Id == Id;
    }
}