using Moonlight.Core.Enums;
using Moonlight.Database.Entities;
using Moonlight.Game.Inventories;
using Moonlight.Game.Inventories.Items;
using Item = Moonlight.Game.Inventories.Items.Item;

namespace Moonlight.Game.Factory.Impl
{
    public class ItemInstanceFactory : IItemInstanceFactory
    {
        private readonly IItemFactory _itemFactory;

        public ItemInstanceFactory(IItemFactory itemFactory) => _itemFactory = itemFactory;

        public ItemInstance CreateItemInstance(int vnum, BagType bagType, int rareOrAmount, int upgrade)
        {
            Item item = _itemFactory.CreateItem(vnum);

            switch (bagType)
            {
                case BagType.EQUIPMENT:
                    return new Equipment(item, (RarityType)rareOrAmount, upgrade);
                default:
                    return new ItemInstance(item, rareOrAmount);
            }
        }
    }
}