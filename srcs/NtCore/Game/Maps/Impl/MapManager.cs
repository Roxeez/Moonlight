using System.Collections.Generic;
using NtCore.Extensions;
using NtCore.Resources;

namespace NtCore.Game.Maps.Impl
{
    public class MapManager : IMapManager
    {
        private readonly IDictionary<int, IMap> _maps;
        private readonly IDictionary<int, byte[]> _data;

        public MapManager()
        {
            _maps = new Dictionary<int, IMap>();
            _data = new Dictionary<int, byte[]>();
        }
        
        public IMap GetMapById(int id)
        {
            IMap map = _maps.GetValueOrDefault(id);
            if (map != null)
            {
                return map;
            }

            byte[] data = _data.GetValueOrDefault(id);
            if (data == null)
            {
                data = Resource.Read($"maps.{id}.bin");
                _data[id] = data;
            }
                
            if (id == 20001)
            {
                return new Miniland(data);
            }

            map = new Map(id, data);
            _maps[id] = map;

            return map;
        }
    }
}