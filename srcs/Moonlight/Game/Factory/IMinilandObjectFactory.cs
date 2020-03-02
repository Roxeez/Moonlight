using Moonlight.Core;
using Moonlight.Game.Inventories.Items;
using Moonlight.Game.Maps;

namespace Moonlight.Game.Factory
{
    internal interface IMinilandObjectFactory
    {
        MinilandObject CreateMinilandObject(int vnum, int slot, Position position);

        MinilandObject CreateMinilandObject(Item item, int slot, Position position);
    }
}