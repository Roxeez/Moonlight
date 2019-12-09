using System.Collections.Generic;
using NtCore.Extensions;

namespace NtCore.Game.Maps.Impl
{
    public class MapManager : IMapManager
    {
        private readonly IDictionary<int, IMap> _maps = new Dictionary<int, IMap>();

        public IMap GetMapById(int id)
        {
            IMap map = _maps.GetValueOrDefault(id);
            if (map == null)
            {
                if (id == 20001)
                {
                    return new Miniland();
                }

                map = new Map(id);
                _maps[id] = map;
            }

            return map;
        }
    }
}