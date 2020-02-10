using Moonlight.Game.Entities;
using Moonlight.Game.Maps;

namespace Moonlight.Event.Maps
{
    public class EntityJoinEvent : IEventNotification
    {
        public Map Map { get; internal set; }
        public Entity Entity { get; internal set; }
    }
}