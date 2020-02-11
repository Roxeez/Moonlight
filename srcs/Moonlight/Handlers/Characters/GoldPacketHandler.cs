using Moonlight.Clients;
using Moonlight.Packet.Character;

namespace Moonlight.Handlers.Characters
{
    internal class GoldPacketHandler : PacketHandler<GoldPacket>
    {
        protected override void Handle(Client client, GoldPacket packet)
        {
            client.Character.Gold = packet.Gold;
        }
    }
}