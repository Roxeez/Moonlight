using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Battle
{
    public class EntityDeathEvent : Event
    {
        public LivingEntity Entity { get; }
        public LivingEntity Killer { get; }
        
        public EntityDeathEvent(IClient client, LivingEntity entity, LivingEntity killer) : base(client)
        {
            Entity = entity;
            Killer = killer;
        }
    }
}