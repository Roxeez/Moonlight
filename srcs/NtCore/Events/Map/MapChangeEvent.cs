using JetBrains.Annotations;
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
        public MapChangeEvent([NotNull] ICharacter character, [NotNull] IMap source, [NotNull] IMap destination)
        {
            Character = character;
            Source = source;
            Destination = destination;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }

        /// <summary>
        ///     Source map
        /// </summary>
        public IMap Source { get; }

        /// <summary>
        ///     Destination map
        /// </summary>
        public IMap Destination { get; }
    }
}