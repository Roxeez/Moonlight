using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Moonlight.Database.Entities;
using Item = Moonlight.Game.Inventories.Items.Item;

namespace Moonlight.Game.Inventories
{
    public class Bag : ReadOnlyCollection<InventoryItem>
    {
        private readonly IList<InventoryItem> _internalItems;
        
        public Bag(IList<InventoryItem> items) : base(items)
        {
            _internalItems = items;
        }

        public InventoryItem GetItemBySlot(int slot)
        {
            return _internalItems.FirstOrDefault(x => x.Slot == slot);
        }

        public InventoryItem GetItemByVnum(int vnum)
        {
            return _internalItems.FirstOrDefault(x => x.Item.Vnum == vnum);
        }

        internal void AddItem(InventoryItem inventoryItem)
        {
            InventoryItem stack = _internalItems.FirstOrDefault(x => x.Slot == inventoryItem.Slot);
            if (stack != null)
            {
                _internalItems.Remove(stack);
            }
            
            _internalItems.Add(inventoryItem);
        }
    }
}