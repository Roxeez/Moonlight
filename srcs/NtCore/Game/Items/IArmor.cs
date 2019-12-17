using System;

namespace NtCore.Game.Items
{
    public interface IArmor : IItem
    {
        byte Rarity { get; }
        byte Upgrade { get; }
    }
}