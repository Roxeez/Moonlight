using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Packet.Character;

namespace Moonlight.Game.Handlers.Characters
{
    internal class SpPacketHandler : PacketHandler<SpPacket>
    {
        protected override void Handle(Client client, SpPacket packet)
        {
            Character character = client.Character;

            character.SpPoints = packet.Points;
            character.AdditionalSpPoints = packet.AdditionalPoints;
        }
    }
}