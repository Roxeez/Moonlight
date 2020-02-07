using Moonlight.Game.Handlers;

namespace Moonlight.Clients
{
    internal interface IClientManager
    {
        Client CreateLocalClient();
    }

    internal class ClientManager : IClientManager
    {
        private readonly IPacketHandlerManager _packetHandlerManager;

        public ClientManager(IPacketHandlerManager packetHandlerManager) => _packetHandlerManager = packetHandlerManager;

        public Client CreateLocalClient()
        {
            Client client = new LocalClient();

            client.PacketReceived += x => _packetHandlerManager.Handle(client, x);
            client.PacketSend += x => _packetHandlerManager.Handle(client, x);

            return client;
        }
    }
}