using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Inventories
{
    public class ItemStack
    {
        internal ItemStack(Item item) => Item = item;

        public Item Item { get; }
        public int Amount { get; internal set; }
    }
}