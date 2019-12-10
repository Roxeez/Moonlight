using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Character
{
    /// <summary>
    ///     Event called when Character target stat update (st packet)
    /// </summary>
    public class TargetStatUpdateEvent : Event
    {
        public TargetStatUpdateEvent([NotNull] IClient client) : base(client) => Character = client.Character;

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public ICharacter Character { get; }
    }
}