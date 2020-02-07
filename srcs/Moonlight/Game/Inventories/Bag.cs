using System.Collections;
using System.Collections.Generic;
using Moonlight.Core.Extensions;

namespace Moonlight.Game.Inventories
{
    public class Bag : IEnumerable<ItemStack>
    {
        private readonly Dictionary<int, ItemStack> _items;

        internal Bag() => _items = new Dictionary<int, ItemStack>();

        public IEnumerator<ItemStack> GetEnumerator() => _items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ItemStack GetItem(int slot) => _items.GetValueOrDefault(slot);

        internal void SetItem(int slot, ItemStack item)
        {
            _items[slot] = item;
        }
    }
}