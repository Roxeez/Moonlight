using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.Extensions;
using NtCore.Game.Inventory;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers.Character
{
    public class PairyPacketHandler : PacketHandler<PairyPacket>
    {
        public override void Handle(IClient client, PairyPacket packet)
        {
            if (packet.EntityType != EntityType.Player)
            {
                return;
            }

            var character = client.Character.AsModifiable();
            if (character.Id != packet.EntityId)
            {
                return;
            }

            var equipment = character.Equipment.AsModifiable();
            equipment.Fairy = new Fairy
            {
                Element = packet.Element,
                Power = packet.Power
            };
        }
    }
}