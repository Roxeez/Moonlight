using System;
using Moonlight.Core.Enums;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Inventories
{
    public class ItemInstance : IEquatable<ItemInstance>
    {
        internal ItemInstance(Item item, BagType bagType, int slot, int amount)
        {
            Item = item;
            BagType = bagType;
            Slot = slot;
            Amount = amount;
        }

        public Item Item { get; }
        public BagType BagType { get; }
        public int Slot { get; }
        public int Amount { get; }

        public bool Equals(ItemInstance other) => other != null && other.Item.Vnum == Item.Vnum && other.BagType == BagType && other.Slot == Slot;
    }
}