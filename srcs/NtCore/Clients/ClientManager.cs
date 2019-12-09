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

        private IClient _localClient;

        public ClientManager(IPacketManager packetManager) => _packetManager = packetManager;

        public IClient CreateLocalClient()
        {
            if (_localClient != null)
            {
                return _localClient;
            }

            Process process = Process.GetCurrentProcess();
            if (process.MainModule == null)
            {
                throw new InvalidOperationException("Process module can't be null");
            }

            var localClient = new LocalClient(process.MainModule);

            localClient.PacketReceived += packet => _packetManager.Handle(localClient, packet, PacketType.Recv);
            localClient.PacketSend += packet => _packetManager.Handle(localClient, packet, PacketType.Send);

            _clients[localClient.Id] = localClient;

            _localClient = localClient;

            return localClient;
        }

        public IClient CreateRemoteClient() => throw new NotImplementedException();

        public IEnumerable<IClient> Clients => _clients.Values;
    }
}