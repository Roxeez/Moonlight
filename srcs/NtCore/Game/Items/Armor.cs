using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Armor : Item
    {
        public Armor(int vnum, string name, ItemInfo itemInfo) : base(vnum, name, itemInfo)
        {
        }

        public byte Rarity { get; internal set; }
        public byte Upgrade { get; internal set; }
    }
}