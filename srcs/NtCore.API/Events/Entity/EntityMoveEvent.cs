using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Entity
{
    public class EntityMoveEvent : Event
    {
        public ILivingEntity Entity { get; }
        public Position From { get; }

        public EntityMoveEvent(ILivingEntity entity, Position from)
        {
            Entity = entity;
            From = from;
        }
    }
}