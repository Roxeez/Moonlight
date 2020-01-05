using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    public class EntityRestEvent : Event
    {
        public LivingEntity Entity { get; }
        
        public EntityRestEvent(IClient client, LivingEntity entity) : base(client)
        {
            Entity = entity;
        }
    }
}