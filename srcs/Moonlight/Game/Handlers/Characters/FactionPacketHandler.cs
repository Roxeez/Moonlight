using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Packet.Character;

namespace Moonlight.Game.Handlers.Characters
{
    internal class FactionPacketHandler : PacketHandler<FsPacket>
    {
        protected override void Handle(Client client, FsPacket packet)
        {
            Character character = client.Character;

            character.Faction = packet.Faction;
        }
    }
}