using Moonlight.Core;
using Moonlight.Game.Maps;

namespace Moonlight.Game.Factory
{
    public interface IMinilandObjectFactory
    {
        MinilandObject CreateMinilandObject(int vnum, int slot, Position position);
    }
}