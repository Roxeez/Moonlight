namespace Moonlight.Database.DAL
{
    internal interface IEntity
    {
    }

    internal interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}