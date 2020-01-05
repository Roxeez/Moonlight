using NtCore.Clients;
using NtCore.Enums;
using NtCore.Game.Items;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class PairyPacketHandler : PacketHandler<PairyPacket>
    {
        public override void Handle(IClient client, PairyPacket packet)
        {
            if (packet.EntityType != EntityType.PLAYER)
            {
                return;
            }

            if (packet.EntityId != client.Character.Id)
            {
                return;
            }

            Fairy fairy = client.Character.Equipment.Fairy;

            if (fairy == null)
            {
                return;
            }

            fairy.Element = packet.Element;
            fairy.Power = packet.Power;
        }
    }
}