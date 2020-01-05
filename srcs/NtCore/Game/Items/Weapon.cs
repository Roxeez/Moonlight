using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Weapon : Item
    {
        public byte Rarity { get; set; }
        public byte Upgrade { get; set; }

        public Weapon(int vnum, string name, ItemInfo itemInfo) : base(vnum, name, itemInfo)
        {
        }
    }
}