using NtCore.API.Client;
using NtCore.API.Events.Maps;
using NtCore.Extensions;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class MlInfoBrPacketHandler : PacketHandler<MlInfoBrPacket>
    {
        private readonly PluginManager _pluginManager;

        public MlInfoBrPacketHandler(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }
        
        public override void Handle(IClient client, MlInfoBrPacket packet)
        {
            var miniland = client.Character.Map.As<Miniland>();

            if (miniland == null)
            {
                return;
            }

            miniland.Owner = packet.Owner;
            miniland.Visitor = packet.Visitor;
            miniland.TotalVisitor = packet.TotalVisitor;
            miniland.Message = packet.Message;
            
            _pluginManager.CallEvent(new MinilandJoinEvent(client, miniland));
        }
    }
}