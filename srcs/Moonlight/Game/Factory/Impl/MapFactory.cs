using System;
using Moonlight.Core.Enums;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Game.Maps;
using Moonlight.Translation;

namespace Moonlight.Game.Factory.Impl
{
    internal class MapFactory : IMapFactory
    {
        private readonly ILanguageService _languageService;
        private readonly IRepository<MapDto> _mapRepository;

        public MapFactory(ILanguageService languageService, IRepository<MapDto> mapRepository)
        {
            _languageService = languageService;
            _mapRepository = mapRepository;
        }

        public Map CreateMap(int mapId)
        {
            MapDto mapDto = _mapRepository.Select(mapId);
            if (mapDto == null)
            {
                throw new InvalidOperationException($"Can't found map with id {mapId} in database.");
            }

            string name = _languageService.GetTranslation(RootKey.MAP, mapDto.NameKey);
            return mapId != 20001 ? new Map(mapDto.Id, name, mapDto.Grid) : new Miniland(name, mapDto.Grid);
        }

        public Miniland CreateMiniland() => (Miniland)CreateMap(20001);
    }
}