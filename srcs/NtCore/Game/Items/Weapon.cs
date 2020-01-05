using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Weapon : Item
    {
        public byte Rarity { get; internal set; }
        public byte Upgrade { get; internal set; }

        public Weapon(int vnum, string name, ItemInfo itemInfo) : base(vnum, name, itemInfo)
        {
        }
    }
}