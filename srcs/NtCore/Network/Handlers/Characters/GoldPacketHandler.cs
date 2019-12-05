using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class GoldPacketHandler : PacketHandler<GoldPacket>
    {
        public override void Handle(IClient client, GoldPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Gold = packet.Gold;
        }
    }
}