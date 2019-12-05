using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class FactionPacketHandler : PacketHandler<FactionPacket>
    {
        public override void Handle(IClient client, FactionPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Faction = packet.Faction;
        }
    }
}