using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class FactionPacketHandler : PacketHandler<FactionPacket>
    {
        public override void Handle(IClient client, FactionPacket packet)
        {
            Character character = client.Character;

            character.Faction = packet.Faction;
        }
    }
}