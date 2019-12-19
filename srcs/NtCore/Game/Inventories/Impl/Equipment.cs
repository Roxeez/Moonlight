using NtCore.Game.Items;

namespace NtCore.Game.Inventories.Impl
{
    public class Equipment : IEquipment
    {
        public IWeapon MainWeapon { get; set; }
        public IArmor Armor { get; set; }
        public IWeapon SecondaryWeapon { get; set; }
        public IFairy Fairy { get; set; }
    }
}