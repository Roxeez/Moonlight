namespace NtCore.Game.Items.Impl
{
    public class Weapon : IWeapon
    {
        public int Vnum { get; set; }
        public string Name { get; set; }
        public byte Rarity { get; set; }
        public byte Upgrade { get; set; }
    }
}