using JetBrains.Annotations;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Map
{
    /// <summary>
    ///     Event called when joining miniland
    ///     This event is called AFTER map initialization
    /// </summary>
    public class MinilandJoinEvent : Event
    {
        public MinilandJoinEvent([NotNull] ICharacter character, [NotNull] IMiniland miniland)
        {
            Character = character;
            Miniland = miniland;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }

        /// <summary>
        ///     Miniland joined
        /// </summary>
        public IMiniland Miniland { get; }
    }
}