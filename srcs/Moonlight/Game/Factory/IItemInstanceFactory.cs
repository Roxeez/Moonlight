using Moonlight.Core.Enums;
using Moonlight.Game.Inventories;

namespace Moonlight.Game.Factory
{
    public interface IItemInstanceFactory
    {
        ItemInstance CreateItemInstance(int vnum, int amount);
    }
}