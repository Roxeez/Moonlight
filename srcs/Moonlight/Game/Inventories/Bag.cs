using System.Linq;
using Moonlight.Core;
using Moonlight.Core.Collection;

namespace Moonlight.Game.Inventories
{
    public class Bag : InternalObservableDictionary<int, ItemInstance>
    {
        public ItemInstance GetItemInSlot(int slot) => this[slot];

        public ItemInstance GetItemWithVnum(int vnum)
        {
            return Values.FirstOrDefault(x => x.Item.Vnum == vnum);
        }

        public int GetSlot(ItemInstance instance)
        {
            return ThreadSafeInternalDictionary.FirstOrDefault(x => x.Value.Equals(instance)).Key;
        }

        internal void AddItem(int slot, ItemInstance item)
        {
            this[slot] = item;
        }

        internal void RemoveItem(int slot)
        {
            Remove(slot);
        }
    }
}