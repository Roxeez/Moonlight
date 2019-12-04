using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers.Character
{
    public class GoldPacketHandler : PacketHandler<GoldPacket>
    {
        public override void Handle(IClient client, GoldPacket packet)
        {
            var character = client.Character.AsModifiable();

            character.Gold = packet.Gold;
        }
    }
}