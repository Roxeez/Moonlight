using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Inventories.Impl;
using NtCore.Game.Items.Impl;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class EquipPacketHandler : PacketHandler<EquipPacket>
    {
        public override void Handle(IClient client, EquipPacket packet)
        {
            var character = client.Character.As<Character>();

            EquipSubPacket mainWeaponSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.MAIN_WEAPON);
            EquipSubPacket armorSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.ARMOR);
            EquipSubPacket secondaryWeaponSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.SECONDARY_WEAPON);
            EquipSubPacket fairySub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.FAIRY);

            var equipment = new Equipment();

            if (mainWeaponSub != null)
            {
                equipment.MainWeapon = new Weapon
                {
                    Vnum = mainWeaponSub.Vnum,
                    Rarity = mainWeaponSub.Rarity,
                    Upgrade = mainWeaponSub.Upgrade
                };
            }

            if (armorSub != null)
            {
                equipment.Armor = new Armor
                {
                    Vnum = armorSub.Vnum,
                    Rarity = armorSub.Rarity,
                    Upgrade = armorSub.Upgrade
                };
            }

            if (secondaryWeaponSub != null)
            {
                equipment.SecondaryWeapon = new Weapon
                {
                    Vnum = secondaryWeaponSub.Vnum,
                    Rarity = secondaryWeaponSub.Rarity,
                    Upgrade = secondaryWeaponSub.Upgrade
                };
            }

            if (fairySub != null)
            {
                equipment.Fairy = new Fairy
                {
                    Vnum = fairySub.Vnum
                };
            }

            character.Equipment = equipment;
        }
    }
}