using System;
using System.Diagnostics;
using NtCore.API.Clients;
using NtCore.Network;

namespace NtCore.Clients
{
    public class ClientManager : IClientManager
    {
        private readonly IPacketManager _packetManager;

        public ClientManager(IPacketManager packetManager)
        {
            _packetManager = packetManager;
        }

        public IClient LocalClient { get; private set; }

        public IClient CreateLocalClient()
        {
            if (LocalClient != null) return LocalClient;

            var process = Process.GetCurrentProcess();

            if (process.MainModule == null) throw new InvalidOperationException("Process module can't be null");

            var localClient = new LocalClient(process.MainModule);

            localClient.PacketReceived += packet => _packetManager.Handle(localClient, packet, PacketType.Recv);
            localClient.PacketSend += packet => _packetManager.Handle(localClient, packet, PacketType.Send);

            LocalClient = localClient;

            return localClient;
        }
    }
}