using NtCore.API.Client;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Maps
{
    public class MinilandJoinEvent : Event
    {
        public IClient Client { get; }
        public IMiniland Miniland { get; }

        public MinilandJoinEvent(IClient client, IMiniland miniland)
        {
            Client = client;
            Miniland = miniland;
        }
    }
}