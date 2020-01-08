using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    /// <summary>
    /// Called when any entity rest state changed
    /// </summary>
    public class EntityRestEvent : Event
    {
        /// <summary>
        /// Entity involved in this event
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }
        
        public EntityRestEvent(IClient client, LivingEntity entity) : base(client)
        {
            Entity = entity;
        }
    }
}