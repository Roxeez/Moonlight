using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moonlight.Core.Extensions;

namespace Moonlight.Database.DAL
{
    internal abstract class GenericRepository<TEntity, TDto, TObjectId, TContext> : IGenericRepository<TObjectId, TDto>
    where TDto : class, IDto<TObjectId>, new() where TEntity : class, IEntity<TObjectId>, new() where TContext : DbContext
    {
        private readonly IContextFactory<TContext> _contextFactory;
        private readonly IMapper<TEntity, TDto> _mapper;

        protected GenericRepository(IContextFactory<TContext> contextFactory, IMapper<TEntity, TDto> mapper)
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

        public virtual TDto Select(TObjectId id)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                TEntity entity = context.Set<TEntity>().FirstOrDefault(x => (object)x.Id == (object)id);
                return _mapper.Map(entity);
            }
        }

        public virtual TDto Insert(TDto obj)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                TEntity entity = _mapper.Map(obj);
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
                return _mapper.Map(entity);
            }
        }

        public virtual IEnumerable<TDto> InsertAll(IEnumerable<TDto> objects)
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                List<TEntity> entities = _mapper.Map(objects.ToList());

                context.Set<TEntity>().AddRange(entities);

                context.SaveChanges();
                return _mapper.Map(entities);
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

        public void Clear()
        {
            using (TContext context = _contextFactory.CreateContext())
            {
                context.Set<TEntity>().RemoveRange(context.Set<TEntity>());
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