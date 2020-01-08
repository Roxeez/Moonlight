using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Battle
{
    /// <summary>
    /// Event called when an entity die
    /// </summary>
    public class EntityDeathEvent : Event
    {
        /// <summary>
        /// Dead entity
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }
        
        /// <summary>
        /// Killer
        /// </summary>
        [NotNull]
        public LivingEntity Killer { get; }
        
        public EntityDeathEvent(IClient client, LivingEntity entity, LivingEntity killer) : base(client)
        {
            Entity = entity;
            Killer = killer;
        }
    }
}