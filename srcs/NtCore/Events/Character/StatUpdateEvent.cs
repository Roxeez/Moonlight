using JetBrains.Annotations;
using NtCore.Clients;

namespace NtCore.Events.Character
{
    /// <summary>
    /// Called when character stat update (hp, mp)
    /// </summary>
    public class StatUpdateEvent : Event
    {
        public StatUpdateEvent(IClient client) : base(client) => Character = client.Character;

        /// <summary>
        /// Character involved in this event
        /// </summary>
        [NotNull]
        public Game.Entities.Character Character { get; }
    }
}