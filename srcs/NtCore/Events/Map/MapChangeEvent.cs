using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Map
{
    /// <summary>
    ///     Event called when character change map BEFORE
    ///     This event is called before map initialization (before receiving in packets)
    /// </summary>
    public class MapChangeEvent : Event
    {
        public MapChangeEvent([NotNull] IClient client, [NotNull] Game.Maps.Map source, [NotNull] Game.Maps.Map destination) : base(client)
        {
            Character = client.Character;
            Source = source;
            Destination = destination;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public Game.Entities.Character Character { get; }

        /// <summary>
        ///     Source map
        /// </summary>
        public Game.Maps.Map Source { get; }

        /// <summary>
        ///     Destination map
        /// </summary>
        public Game.Maps.Map Destination { get; }
    }
}