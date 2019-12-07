using JetBrains.Annotations;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Map
{
    /// <summary>
    ///     Event called when client change map BEFORE receiving all in packets
    /// </summary>
    public class MapChangeEvent : Event
    {
        public MapChangeEvent([NotNull] ICharacter character, [NotNull] IMap source, [NotNull] IMap destination)
        {
            Character = character;
            Source = source;
            Destination = destination;
        }

        public ICharacter Character { get; }
        public IMap Source { get; }
        public IMap Destination { get; }
    }
}