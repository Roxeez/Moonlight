namespace Moonlight.Database.DAL
{
    public interface IDto
    {
    }

    public interface IDto<T> : IDto
    {
        T Id { get; set; }
    }
}