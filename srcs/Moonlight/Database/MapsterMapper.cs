using System.Collections.Generic;
using Mapster;
using Moonlight.Database.DAL;

namespace Moonlight.Database
{
    internal class MapsterMapper<TEntity, TDto> : IMapper<TEntity, TDto> where TEntity : IEntity where TDto : IDto
    {
        public TEntity Map(TDto input) => input.Adapt<TEntity>();

        public List<TEntity> Map(List<TDto> input) => input.Adapt<List<TEntity>>();

        public TDto Map(TEntity input) => input.Adapt<TDto>();

        public List<TDto> Map(List<TEntity> input) => input.Adapt<List<TDto>>();

        public IEnumerable<TEntity> Map(IEnumerable<TDto> input) => input.Adapt<IEnumerable<TEntity>>();

        public IEnumerable<TDto> Map(IEnumerable<TEntity> input) => input.Adapt<IEnumerable<TDto>>();
    }
}