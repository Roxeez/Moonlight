using JetBrains.Annotations;
using NtCore.Game.Entities;

namespace NtCore.Events.Character
{
    /// <summary>
    ///     Event called when Character target moved
    /// </summary>
    public class TargetMoveEvent : Event
    {
        public TargetMoveEvent([NotNull] ICharacter character, Position from)
        {
            Character = character;
            From = from;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }

        /// <summary>
        ///     Position of target before movement
        /// </summary>
        public Position From { get; }
    }
}