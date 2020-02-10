using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Moonlight.Core;
using Moonlight.Core.Extensions;
using Moonlight.Database.Entities;
using Item = Moonlight.Game.Inventories.Items.Item;

namespace Moonlight.Game.Inventories
{
    public class Bag : SafeObservableCollection<ItemInstance>
    {
        public ItemInstance GetItemBySlot(int slot)
        {
            return this.FirstOrDefault(x => x.Slot == slot);
        }

        public ItemInstance GetItemByVnum(int vnum)
        {
            return this.FirstOrDefault(x => x.Item.Vnum == vnum);
        }

        internal void RemoveItemInSlot(int slot)
        {
            ItemInstance itemInstance = this.FirstOrDefault(x => x.Slot == slot);
            if (itemInstance == null)
            {
                return;
            }

            Remove(itemInstance);
        }

        internal void AddItem(ItemInstance itemInstance)
        {
            ItemInstance stack = this.FirstOrDefault(x => x.Slot == itemInstance.Slot);
            if (stack != null)
            {
                Remove(stack);
            }
            
            Add(itemInstance);
        }
    }
}