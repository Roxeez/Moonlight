using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Factory;
using NtCore.Game.Inventories;
using NtCore.Game.Items;
using NtCore.Network.Packets.Characters;
using NtCore.Registry;

namespace NtCore.Network.Handlers.Characters
{
    public class EquipPacketHandler : PacketHandler<EquipPacket>
    {
        private readonly IItemFactory _itemFactory;

        public EquipPacketHandler(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        public override void Handle(IClient client, EquipPacket packet)
        {
            Character character = client.Character;

            EquipSubPacket mainWeaponSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.MAIN_WEAPON);
            EquipSubPacket armorSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.ARMOR);
            EquipSubPacket secondaryWeaponSub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.SECONDARY_WEAPON);
            EquipSubPacket fairySub = packet.EquipSubPackets.GetValueOrDefault(EquipmentType.FAIRY);

            var equipment = new Equipment();

            if (mainWeaponSub != null)
            {
                equipment.MainWeapon = _itemFactory.CreateWeapon(mainWeaponSub.Vnum, mainWeaponSub.Rarity, mainWeaponSub.Upgrade);
            }

            if (armorSub != null)
            {
                equipment.Armor = _itemFactory.CreateArmor(armorSub.Vnum, armorSub.Rarity, armorSub.Upgrade);
            }

            if (secondaryWeaponSub != null)
            {
                equipment.SecondaryWeapon = _itemFactory.CreateWeapon(secondaryWeaponSub.Vnum, secondaryWeaponSub.Rarity, secondaryWeaponSub.Upgrade);
            }

            if (fairySub != null)
            {
                equipment.Fairy = _itemFactory.CreateFairy(fairySub.Vnum);
            }

            character.Equipment = equipment;
        }
    }
}