using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Maps;

namespace NtCore.Events.Map
{
    /// <summary>
    ///     Event called when joining miniland
    ///     This event is called AFTER map initialization
    /// </summary>
    public class MinilandJoinEvent : Event
    {
        public MinilandJoinEvent([NotNull] IClient client, [NotNull] Miniland miniland) : base(client)
        {
            Character = client.Character;
            Miniland = miniland;
        }

        /// <summary>
        ///     Character involved in this event
        /// </summary>
        public Game.Entities.Character Character { get; }

        /// <summary>
        ///     Miniland joined
        /// </summary>
        public Miniland Miniland { get; }
    }
}