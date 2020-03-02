using System.Collections.Generic;
using Moonlight.Core;
using Moonlight.Extensions.Game;
using Moonlight.Game.Inventories.Items;
using Moonlight.Game.Maps;

namespace Moonlight.Game.Factory.Impl
{
    internal class MinilandObjectFactory : IMinilandObjectFactory
    {
        private readonly IItemFactory _itemFactory;

        public MinilandObjectFactory(IItemFactory itemFactory) => _itemFactory = itemFactory;

        public MinilandObject CreateMinilandObject(int vnum, int slot, Position position)
        {
            Item item = _itemFactory.CreateItem(vnum);
            return item.IsMinigame() ? new Minigame(item, slot, position) : new MinilandObject(item, slot, position);
        }

        public MinilandObject CreateMinilandObject(Item item, int slot, Position position)
        {
            return item.IsMinigame() ? new Minigame(item, slot, position) : new MinilandObject(item, slot, position);
        }
    }
}