using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
        IMap Map { get; set; }
        Position Position { get; set; }
    }
}