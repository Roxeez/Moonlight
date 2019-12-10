using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Map
{
    /// <summary>
    ///     Event called when joining miniland
    ///     This event is called AFTER map initialization
    /// </summary>
    public class MinilandJoinEvent : Event
    {
        public MinilandJoinEvent([NotNull] IClient client, [NotNull] IMiniland miniland) : base(client)
        {
            Character = client.Character;
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