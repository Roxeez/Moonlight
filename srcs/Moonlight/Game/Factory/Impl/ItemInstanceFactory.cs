using Moonlight.Game.Inventories;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Factory.Impl
{
    public class ItemInstanceFactory : IItemInstanceFactory
    {
        private readonly IItemFactory _itemFactory;

        public ItemInstanceFactory(IItemFactory itemFactory) => _itemFactory = itemFactory;

        public ItemInstance CreateItemInstance(int vnum, int amount)
        {
            Item item = _itemFactory.CreateItem(vnum);

            return new ItemInstance(item, amount == 0 ? 1 : amount);
        }
    }
}