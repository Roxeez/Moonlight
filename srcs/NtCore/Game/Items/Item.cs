namespace NtCore.Game.Items.Impl
{
    public class Item : IItem
    {
        public int Vnum { get; }
        public string Name { get; }

        public Item(int vnum, string name)
        {
            Vnum = vnum;
            Name = name;
        }
    }
}