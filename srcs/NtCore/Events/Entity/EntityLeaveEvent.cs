using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    /// <summary>
    /// Event called when an entity leave the map
    /// </summary>
    public class EntityLeaveEvent : Event
    {
        public EntityLeaveEvent([NotNull] IClient client, [NotNull] LivingEntity entity, [NotNull] Game.Maps.Map map) : base(client)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }

        /// <summary>
        ///     Map where entity leaved
        /// </summary>
        [NotNull]
        public Game.Maps.Map Map { get; }
    }
}