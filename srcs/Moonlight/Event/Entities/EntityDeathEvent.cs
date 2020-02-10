using Moonlight.Game.Entities;

namespace Moonlight.Event.Entities
{
    public class EntityDeathEvent : IEventNotification
    {
        public LivingEntity Entity { get; set; }
        public LivingEntity Killer { get; set; }
    }
}