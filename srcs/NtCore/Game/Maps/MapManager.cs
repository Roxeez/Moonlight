using System.Collections.Generic;
using NtCore.Extensions;
using NtCore.I18N;
using NtCore.Registry;
using NtCore.Resources;

namespace NtCore.Game.Maps
{
    public class MapManager : IMapManager
    {
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;
        private readonly IDictionary<int, byte[]> _maps;
        public MapManager(ILanguageService languageService, IRegistry registry)
        {
            _maps = new Dictionary<int, byte[]>();
            _languageService = languageService;
            _registry = registry;
        }

        public Map GetMapById(int id)
        {
            byte[] data = _maps.GetValueOrDefault(id);
            if (data == null)
            {
                data = ResourceManager.Read($"maps.{id}");
                _maps[id] = data;
            }
            
            MapInfo info = _registry.GetMapInfo(id);
            if (id == 20001)
            {
                return new Miniland(data)
                {
                    Name = info != null ? _languageService.GetTranslation(LanguageKey.MAP, info.NameKey) : $"{id}"
                };
            }

            var map = new Map(id, data)
            {
                Name = info != null ? _languageService.GetTranslation(LanguageKey.MAP, info.NameKey) : $"{id}"
            };

            return map;
        }
    }
}