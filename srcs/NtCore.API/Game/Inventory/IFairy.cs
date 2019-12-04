using NtCore.API.Enums;

namespace NtCore.API.Game.Inventory
{
    public interface IFairy
    {
        Element Element { get; }
        int Power { get; }
    }
}