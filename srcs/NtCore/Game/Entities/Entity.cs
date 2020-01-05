using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public abstract class Entity
    {
        public int Id { get; internal set; }
        public EntityType EntityType { get; internal set; }
        public Map Map { get; internal set; }
        public Position Position { get; internal set; }
    }
}