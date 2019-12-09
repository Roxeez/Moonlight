using JetBrains.Annotations;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Entity
{
    public class EntityLeaveEvent : Event
    {
        public EntityLeaveEvent([NotNull] IEntity entity, [NotNull] IMap map)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        public IEntity Entity { get; }

        /// <summary>
        ///     Map where entity leaved
        /// </summary>
        public IMap Map { get; }
    }
}