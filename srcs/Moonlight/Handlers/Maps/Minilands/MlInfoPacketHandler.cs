using Moonlight.Clients;
using Moonlight.Packet.Map.Miniland;

namespace Moonlight.Handlers.Maps.Minilands
{
    internal class MlInfoPacketHandler : PacketHandler<MlInfoPacket>
    {
        protected override void Handle(Client client, MlInfoPacket packet)
        {
            client.Character.ProductionPoints = packet.Points;
        }
    }
}