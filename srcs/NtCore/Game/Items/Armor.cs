using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Armor : Item
    {
        public byte Rarity { get; set; }
        public byte Upgrade { get; set; }

        public Armor(int vnum, string name, ItemInfo itemInfo) : base(vnum, name, itemInfo)
        {
        }
    }
}