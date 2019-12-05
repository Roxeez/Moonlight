using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Inventory;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class PairyPacketHandler : PacketHandler<PairyPacket>
    {
        public override void Handle(IClient client, PairyPacket packet)
        {
            if (packet.EntityType != EntityType.Player)
            {
                return;
            }

            var character = client.Character.As<Character>();
            if (character.Id != packet.EntityId)
            {
                return;
            }

            var equipment = character.Equipment.As<Equipment>();
            equipment.Fairy = new Fairy
            {
                Element = packet.Element,
                Power = packet.Power
            };
        }
    }
}