using JetBrains.Annotations;
using NtCore.API.Client;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Maps
{
    /// <summary>
    /// Event called when client change map BEFORE receiving all in packets
    /// </summary>
    public class MapChangeEvent : Event
    {
        public IClient Client { get; }
        public IMap Source { get; }
        public IMap Destination { get; }

        public MapChangeEvent([NotNull] IClient client, [NotNull] IMap source, [NotNull] IMap destination)
        {
            Client = client;
            Source = source;
            Destination = destination;
        }
    }
}