namespace Moonlight.Game.Inventories.Items
{
    public class Item
    {
        internal Item(int vnum, string name)
        {
            Vnum = vnum;
            Name = name;
        }

        public int Vnum { get; }
        public string Name { get; }
    }
}