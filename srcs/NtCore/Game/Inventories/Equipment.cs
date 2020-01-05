using NtCore.Game.Items;

namespace NtCore.Game.Inventories
{
    public class Equipment
    {
        public Weapon MainWeapon { get; internal set; }
        public Armor Armor { get; internal set; }
        public Weapon SecondaryWeapon { get; internal set; }
        public Fairy Fairy { get; internal set; }
    }
}