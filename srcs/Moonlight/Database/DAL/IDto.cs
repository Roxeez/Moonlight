namespace Moonlight.Database.DAL
{
    internal interface IDto
    {
    }

    internal interface IDto<T> : IDto
    {
        T Id { get; set; }
    }
}