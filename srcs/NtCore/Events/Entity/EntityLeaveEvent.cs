using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Entity
{
    public class EntityLeaveEvent : Event
    {
        public EntityLeaveEvent([NotNull] IClient client, [NotNull] ILivingEntity entity, [NotNull] IMap map) : base(client)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        public ILivingEntity Entity { get; }

        /// <summary>
        ///     Map where entity leaved
        /// </summary>
        public IMap Map { get; }
    }
}