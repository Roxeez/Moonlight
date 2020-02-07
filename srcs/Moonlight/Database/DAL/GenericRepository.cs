using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moonlight.Core.Extensions;

namespace Moonlight.Database.DAL
{
    internal class GenericRepository<TEntity, TDto, TObjectId, TContext> : IGenericRepository<TObjectId, TDto>
    where TDto : class, IDto<TObjectId>, new() where TEntity : class, IEntity<TObjectId>, new() where TContext : DbContext
    {
        private readonly IContextFactory<TContext> _contextFactory;
        private readonly IMapper<TEntity, TDto> _mapper;

        public GenericRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                var entities = context.Set<TEntity>().ToList();
                return _mapper.Map(entities);
            }
        }

        public virtual TDto Find(TObjectId id)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                TEntity entity = context.Set<TEntity>().FirstOrDefault(x => (object)x.Id == (object)id);
                return _mapper.Map(entity);
            }
        }

        public virtual TDto Save(TDto obj)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                TEntity entity = context.Set<TEntity>().Find(obj.Id);

                if (entity == null)
                {
                    entity = _mapper.Map(obj);
                    entity = context.Set<TEntity>().Add(entity);
                }
                else
                {
                    context.Entry(entity).CurrentValues.SetValues(obj);
                }

                context.SaveChanges();
                return _mapper.Map(entity);
            }
        }

        public virtual IEnumerable<TDto> SaveAll(IEnumerable<TDto> objects)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                var result = new List<TEntity>();
                var entities = context.Set<TEntity>().ToDictionary(x => x.Id);

                foreach (TDto dto in objects)
                {
                    TEntity entity = entities.GetValueOrDefault(dto.Id);
                    if (entity == default)
                    {
                        entity = _mapper.Map(dto);
                        context.Set<TEntity>().Add(entity);
                    }
                    else
                    {
                        context.Entry(entity).CurrentValues.SetValues(dto);
                    }

                    result.Add(entity);
                }

                context.SaveChanges();
                return _mapper.Map(result);
            }
        }

        public virtual void Delete(TObjectId id)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                TEntity entity = context.Set<TEntity>().Find(id);
                if (entity == null)
                {
                    return;
                }

                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }
    }

    internal class Repository<TEntity, TDto, TContext> : GenericRepository<TEntity, TDto, int, TContext>, IRepository<TDto>
    where TDto : class, IDto<int>, new() where TEntity : class, IEntity<int>, new() where TContext : DbContext
    {
        public Repository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper) : base(contextFactory, mapper)
        {
        }
    }

    internal class StringRepository<TEntity, TDto, TContext> : GenericRepository<TEntity, TDto, string, TContext>, IStringRepository<TDto>
    where TDto : class, IDto<string>, new() where TEntity : class, IEntity<string>, new() where TContext : DbContext
    {
        public StringRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper) : base(contextFactory, mapper)
        {
        }
    }
}