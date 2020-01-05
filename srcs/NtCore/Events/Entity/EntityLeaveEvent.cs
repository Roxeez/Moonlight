using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Entity
{
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
        public LivingEntity Entity { get; }

        /// <summary>
        ///     Map where entity leaved
        /// </summary>
        public Game.Maps.Map Map { get; }
    }
}