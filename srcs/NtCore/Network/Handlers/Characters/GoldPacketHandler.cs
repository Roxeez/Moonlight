using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class GoldPacketHandler : PacketHandler<GoldPacket>
    {
        public override void Handle(IClient client, GoldPacket packet)
        {
            Character character = client.Character;

            character.Gold = packet.Gold;
        }
    }
}