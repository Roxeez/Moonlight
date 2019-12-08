using JetBrains.Annotations;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Character
{
    /// <summary>
    ///     Event called when Character target stat update (st packet)
    /// </summary>
    public class TargetStatUpdateEvent : Event
    {
        public TargetStatUpdateEvent([NotNull] ICharacter character) => Character = character;

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }
    }
}