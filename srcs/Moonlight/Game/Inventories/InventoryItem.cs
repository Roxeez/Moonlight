using System;
using Moonlight.Core.Enums;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Inventories
{
    public class InventoryItem : IEquatable<InventoryItem>
    {
        internal InventoryItem(Item item, BagType bagType, int slot)
        {
            Item = item;
            BagType = bagType;
            Slot = slot;
        }

        public Item Item { get; }
        public BagType BagType { get; }
        public int Slot { get; }
        public int Amount { get; internal set; }

        public bool Equals(InventoryItem other) => other != null && other.Item.Vnum == Item.Vnum && other.BagType == BagType && other.Slot == Slot;
    }
}