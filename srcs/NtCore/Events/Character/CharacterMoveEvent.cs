using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Core;

namespace NtCore.Events.Character
{
    /// <summary>
    ///     Event called on Character move
    ///     Basically called on walk packet
    /// </summary>
    public class CharacterMoveEvent : Event
    {
        public CharacterMoveEvent([NotNull] IClient client, Position from) : base(client)
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
        ///     Position of character before he moved
        /// </summary>
        public Position From { get; }
    }
}