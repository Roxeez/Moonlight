using System;
using System.Collections.Generic;
using System.Diagnostics;
using NtCore.Network;

namespace NtCore.Clients
{
    public class ClientManager : IClientManager
    {
        private readonly IDictionary<Guid, IClient> _clients = new Dictionary<Guid, IClient>();
        private readonly IPacketManager _packetManager;

        public ClientManager(IPacketManager packetManager) => _packetManager = packetManager;

        public IClient CreateLocalClient()
        {
            if (LocalClient != null)
            {
                return LocalClient;
            }

            var localClient = new LocalClient();

            localClient.PacketReceived += packet => _packetManager.Handle(localClient, packet, PacketType.Recv);
            localClient.PacketSend += packet => _packetManager.Handle(localClient, packet, PacketType.Send);

            _clients[localClient.Id] = localClient;

            LocalClient = localClient;

            return localClient;
        }

        public IClient CreateRemoteClient() => throw new NotImplementedException();

        public IClient LocalClient { get; private set; }
        public IEnumerable<IClient> Clients => _clients.Values;
    }
}