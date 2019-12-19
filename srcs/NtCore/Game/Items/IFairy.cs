using NtCore.Enums;

namespace NtCore.Game.Items
{
    public interface IFairy : IItem
    {
        Element Element { get; }
        short Power { get; }
    }
}