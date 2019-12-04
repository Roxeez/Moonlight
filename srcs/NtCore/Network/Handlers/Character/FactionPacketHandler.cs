using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers.Character
{
    public class FactionPacketHandler : PacketHandler<FactionPacket>
    {
        public override void Handle(IClient client, FactionPacket packet)
        {
            var character = client.Character.AsModifiable();

            character.Faction = packet.Faction;
        }
    }
}