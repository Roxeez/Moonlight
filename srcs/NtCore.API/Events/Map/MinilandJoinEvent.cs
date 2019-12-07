using JetBrains.Annotations;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Map
{
    /// <summary>
    ///     Event called when joining miniland (AFTER receiving in packets)
    /// </summary>
    public class MinilandJoinEvent : Event
    {
        public MinilandJoinEvent([NotNull] ICharacter character, [NotNull] IMiniland miniland)
        {
            Character = character;
            Miniland = miniland;
        }

        public ICharacter Character { get; }
        public IMiniland Miniland { get; }
    }
}