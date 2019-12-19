using NtCore.Enums;

namespace NtCore.Game.Items.Impl
{
    public class Fairy : IFairy
    {
        public int Vnum { get; set; }
        public Element Element { get; set; }
        public short Power { get; set; }
    }
}