namespace Moonlight.Database.DAL
{
    public interface IEntity
    {
    }

    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}