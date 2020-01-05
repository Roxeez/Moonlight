using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public abstract class Entity
    {
        public int Id { get; internal set; }
        public EntityType EntityType { get; internal set; }
    }
}