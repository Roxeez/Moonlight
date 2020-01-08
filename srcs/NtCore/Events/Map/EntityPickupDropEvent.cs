using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Map
{
    /// <summary>
    /// Event called when an entity pickup a drop
    /// </summary>
    public class EntityPickupDropEvent : Event
    {
        public EntityPickupDropEvent(IClient client, LivingEntity entity, Drop drop) : base(client)
        {
            Entity = entity;
            Drop = drop;
        }

        /// <summary>
        /// Entity how picked up the drop
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }
        
        /// <summary>
        /// Drop involved in this event
        /// </summary>
        [NotNull]
        public Drop Drop { get; }
    }
}