using System.Collections.Generic;

namespace Moonlight.Database.DAL
{
    public interface IMapper<TEntity, TDto> where TEntity : IEntity where TDto : IDto
    {
        TEntity Map(TDto input);
        List<TEntity> Map(List<TDto> input);
        TDto Map(TEntity input);
        List<TDto> Map(List<TEntity> input);
    }
}