using Moonlight.Game.Maps;

namespace Moonlight.Game.Factory
{
    public interface IMapFactory
    {
        Map CreateMap(int mapId);
        Miniland CreateMiniland();
    }
}