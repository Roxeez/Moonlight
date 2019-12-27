using System.Collections.Generic;
using NtCore.Extensions;
using NtCore.I18N;
using NtCore.Registry;
using NtCore.Resources;

namespace NtCore.Game.Maps.Impl
{
    public class MapManager : IMapManager
    {
        private readonly IDictionary<int, IMap> _maps;
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;
        
        public MapManager(ILanguageService languageService, IRegistry registry)
        {
            _maps = new Dictionary<int, IMap>();
            _languageService = languageService;
            _registry = registry;
        }
        
        public IMap GetMapById(int id)
        {
            IMap map = _maps.GetValueOrDefault(id);
            if (map != null)
            {
                return map;
            }

            MapInfo info = _registry.GetMapInfo(id);
            byte[] data = ResourceManager.Read($"maps.{id}");

            if (id == 20001)
            {
                return new Miniland(data)
                {
                    Name = info != null ? _languageService.GetTranslation(LanguageKey.MAP, info.NameKey) : $"{id}"
                };
            }

            map = new Map(id, data)
            {
                Name = info != null ? _languageService.GetTranslation(LanguageKey.MAP, info.NameKey) : $"{id}"
            };
            _maps[id] = map;

            return map;
        }
    }
}