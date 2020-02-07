using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Factory
{
    public interface IItemFactory
    {
        Item CreateItem(int vnum);
    }
}