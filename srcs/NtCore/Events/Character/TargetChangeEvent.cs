using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Character
{
    /// <summary>
    ///     Event called on Character target change
    ///     Basically called when you click a LivingEntity and receive first St packet
    /// </summary>
    public class TargetChangeEvent : Event
    {
        public TargetChangeEvent([NotNull] IClient client) : base(client)
        {
            Character = client.Character;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }
    }
}