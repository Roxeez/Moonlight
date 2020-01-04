using NtCore.Enums;

namespace NtCore.Game.Items.Impl
{
    public class Fairy : IFairy
    {
        public int Vnum { get; set; }
        public string Name { get; }
        public Element Element { get; set; }
        public short Power { get; set; }
    }
}