using NtCore.API.Client;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Maps
{
    public class MapChangeEvent : Event
    {
        public IClient Client { get; }
        
        public IMap Source { get; }
        public IMap Destination { get; }

        public MapChangeEvent(IClient client, IMap source, IMap destination)
        {
            Client = client;
            Source = source;
            Destination = destination;
        }
    }
}