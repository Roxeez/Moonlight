using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Core;

namespace NtCore.Events.Character
{
    /// <summary>
    ///     Event called when Character target moved
    /// </summary>
    public class TargetMoveEvent : Event
    {
        public TargetMoveEvent([NotNull] IClient client, Position from) : base(client)
        {
            Character = client.Character;
            From = from;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        [NotNull]
        public Game.Entities.Character Character { get; }

        /// <summary>
        ///     Position of target before movement
        /// </summary>
        public Position From { get; }
    }
}