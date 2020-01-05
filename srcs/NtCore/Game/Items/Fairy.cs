using NtCore.Enums;
using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Fairy : Item
    {
        public Element Element { get; internal set; }
        public short Power { get; internal set; }

        public Fairy(int vnum, string name, ItemInfo itemInfo) : base(vnum, name, itemInfo)
        {
        }
    }
}