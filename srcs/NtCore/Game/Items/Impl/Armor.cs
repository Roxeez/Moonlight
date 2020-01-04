namespace NtCore.Game.Items.Impl
{
    public class Armor : IArmor
    {
        public int Vnum { get; set; }
        public string Name { get; }
        public byte Rarity { get; set; }
        public byte Upgrade { get; set; }
    }
}