using Moonlight.Game.Entities;
using Moonlight.Game.Maps;

namespace Moonlight.Event.Maps
{
    public class MapChangeEvent : IEventNotification
    {
        public Character Character { get; internal set; }
        public Map Source { get; internal set; }
        public Map Destination { get; internal set; }
    }
}