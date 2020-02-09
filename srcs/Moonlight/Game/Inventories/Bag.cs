using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Moonlight.Database.Entities;
using Item = Moonlight.Game.Inventories.Items.Item;

namespace Moonlight.Game.Inventories
{
    public class Bag : ReadOnlyObservableCollection<ItemInstance>
    {
        private readonly IList<ItemInstance> _internalItems;
        
        public Bag(ObservableCollection<ItemInstance> items) : base(items)
        {
            _internalItems = items;
        }

        public ItemInstance GetItemBySlot(int slot)
        {
            return _internalItems.FirstOrDefault(x => x.Slot == slot);
        }

        public ItemInstance GetItemByVnum(int vnum)
        {
            return _internalItems.FirstOrDefault(x => x.Item.Vnum == vnum);
        }

        internal void RemoveItemInSlot(int slot)
        {
            ItemInstance itemInstance = _internalItems.FirstOrDefault(x => x.Slot == slot);
            if (itemInstance == null)
            {
                return;
            }

            _internalItems.Remove(itemInstance);
        }

        internal void AddItem(ItemInstance itemInstance)
        {
            ItemInstance stack = _internalItems.FirstOrDefault(x => x.Slot == itemInstance.Slot);
            if (stack != null)
            {
                _internalItems.Remove(stack);
            }
            
            _internalItems.Add(itemInstance);
        }
    }
}