using System.Collections.Generic;
using System.Data.Entity;
using Moonlight.Core.Extensions;

namespace Moonlight.Database.DAL
{
    public abstract class GenericCachedRepository<TEntity, TDto, TObjectId, TContext> : GenericRepository<TEntity, TDto, TObjectId, TContext>
    where TDto : class, IDto<TObjectId>, new() where TEntity : class, IEntity<TObjectId>, new() where TContext : DbContext
    {
        private readonly Dictionary<TObjectId, TDto> _cache;

        protected GenericCachedRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper) : base(contextFactory, mapper) => _cache = new Dictionary<TObjectId, TDto>();

        public override TDto Select(TObjectId id)
        {
            TDto dto = _cache.GetValueOrDefault(id);
            if (dto == default)
            {
                dto = base.Select(id);
                _cache[id] = dto;
            }

            return dto;
        }

        public override void Delete(TObjectId id)
        {
            _cache.Remove(id);
            base.Delete(id);
        }
    }

    public class CachedRepository<TEntity, TDto, TContext> : GenericCachedRepository<TEntity, TDto, int, TContext>, IRepository<TDto>
    where TDto : class, IDto<int>, new() where TEntity : class, IEntity<int>, new() where TContext : DbContext
    {
        public CachedRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper) : base(contextFactory, mapper)
        {
        }
    }

    public class CachedStringRepository<TEntity, TDto, TContext> : GenericCachedRepository<TEntity, TDto, string, TContext>, IStringRepository<TDto>
    where TDto : class, IDto<string>, new() where TEntity : class, IEntity<string>, new() where TContext : DbContext
    {
        public CachedStringRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper) : base(contextFactory, mapper)
        {
        }
    }
}