using NtCore.Registry;

namespace NtCore.Game.Items
{
    public class Item
    {
        public int Vnum { get; }
        public string Name { get; }

        public ItemInfo Info { get; }
        
        public Item(int vnum, string name, ItemInfo itemInfo)
        {
            Vnum = vnum;
            Name = name;
            Info = itemInfo;
        }
    }
}