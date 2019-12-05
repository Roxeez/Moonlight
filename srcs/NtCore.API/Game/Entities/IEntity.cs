using NtCore.API.Enums;
using NtCore.API.Game.Maps;

namespace NtCore.API.Game.Entities
{
    public interface IEntity
    {
        int Id { get; }
        IMap Map { get; }
        Position Position { get; }
        EntityType EntityType { get; }
    }
}