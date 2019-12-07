using JetBrains.Annotations;
using NtCore.API.Clients;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Map
{
    /// <summary>
    ///     Event called when joining miniland (AFTER receiving in packets)
    /// </summary>
    public class MinilandJoinEvent : Event
    {
        public MinilandJoinEvent([NotNull] IClient client, [NotNull] IMiniland miniland)
        {
            Client = client;
            Miniland = miniland;
        }

        public IClient Client { get; }
        public IMiniland Miniland { get; }
    }
}