using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Map
{
    public class EntityPickupDropEvent : Event
    {
        public ILivingEntity Entity { get; set; }
        public IDrop Drop { get; set; }
        

        public EntityPickupDropEvent(IClient client, ILivingEntity entity, IDrop drop) : base(client)
        {
            Entity = entity;
            Drop = drop;
        }
    }
}