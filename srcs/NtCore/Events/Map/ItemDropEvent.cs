using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Map
{
    /// <summary>
    /// Event called when a drop spawn
    /// </summary>
    public class ItemDropEvent : Event
    {
        public ItemDropEvent(IClient client, Drop drop) : base(client) => Drop = drop;

        /// <summary>
        /// Drop involved in this event
        /// </summary>
        [NotNull]
        public Drop Drop { get; }
    }
}