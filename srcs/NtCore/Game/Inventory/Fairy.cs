using NtCore.API.Enums;
using NtCore.API.Game.Inventory;

namespace NtCore.Game.Inventory
{
    public class Fairy : IFairy
    {
        public Element Element { get; set; }
        public int Power { get; set; }
    }
}