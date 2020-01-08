using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Battle
{
    /// <summary>
    /// Event called when an entity is damaged
    /// </summary>
    public class EntityDamageEvent : Event
    {
        public EntityDamageEvent(IClient client, LivingEntity target, LivingEntity caster, int damage) : base(client)
        {
            Target = target;
            Caster = caster;
            Damage = damage;
        }

        /// <summary>
        /// Entity damaged
        /// </summary>
        [NotNull]
        public LivingEntity Target { get; }
        
        /// <summary>
        /// Entity who damaged another entity
        /// </summary>
        [NotNull]
        public LivingEntity Caster { get; }
        
        /// <summary>
        /// Damage deal
        /// </summary>
        public int Damage { get; }
    }
}