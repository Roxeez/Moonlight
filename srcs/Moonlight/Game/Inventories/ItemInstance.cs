using System;
using Moonlight.Core.Enums;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Inventories
{
    public class ItemInstance : IEquatable<ItemInstance>
    {
        public Guid Id { get; }
        
        internal ItemInstance(Item item, int amount)
        {
            Id = Guid.NewGuid();
            Item = item;
            Amount = amount;
        }

        public Item Item { get; }
        public int Amount { get; }

        public bool Equals(ItemInstance other) => other != null && other.Id.Equals(Id);
    }
}