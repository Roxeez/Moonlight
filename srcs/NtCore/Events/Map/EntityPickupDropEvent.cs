using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Map
{
    public class EntityPickupDropEvent : Event
    {
        public EntityPickupDropEvent(IClient client, LivingEntity entity, Drop drop) : base(client)
        {
            Entity = entity;
            Drop = drop;
        }

        public LivingEntity Entity { get; }
        public Drop Drop { get; }
    }
}