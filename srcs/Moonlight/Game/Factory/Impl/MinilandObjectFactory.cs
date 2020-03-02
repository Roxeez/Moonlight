using System.Collections.Generic;
using Moonlight.Core;
using Moonlight.Game.Inventories.Items;
using Moonlight.Game.Maps;

namespace Moonlight.Game.Factory.Impl
{
    internal class MinilandObjectFactory : IMinilandObjectFactory
    {
        // TODO : Use Item#Data for defining if a game is a minigame instead of this static HashSet
        private static readonly HashSet<int> MinigameId;
        private readonly IItemFactory _itemFactory;

        static MinilandObjectFactory()
        {
            MinigameId = new HashSet<int>();
            for (int i = 3117; i <= 3128; i++)
            {
                MinigameId.Add(i);
            }
        }

        public MinilandObjectFactory(IItemFactory itemFactory) => _itemFactory = itemFactory;

        public MinilandObject CreateMinilandObject(int vnum, int slot, Position position)
        {
            Item item = _itemFactory.CreateItem(vnum);
            return MinigameId.Contains(vnum) ? new Minigame(item, slot, position) : new MinilandObject(item, slot, position);
        }

        public MinilandObject CreateMinilandObject(Item item, int slot, Position position)
        {
            return MinigameId.Contains(item.Vnum) ? new Minigame(item, slot, position) : new MinilandObject(item, slot, position);
        }
    }
}