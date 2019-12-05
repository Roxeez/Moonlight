using NtCore.API.Client;
using NtCore.API.Events.Maps;
using NtCore.API.Game.Maps;
using NtCore.API.Managers;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly PluginManager _pluginManager;
        private readonly IMapManager _mapManager;
        
        public CMapPacketHandler(IMapManager mapManager, PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
            _mapManager = mapManager;
        }
        
        public override void Handle(IClient client, CMapPacket packet)
        {
            if (!packet.IsJoining)
            {
                return;
            }

            var character = client.Character.As<Character>();
            var source = client.Character.Map.As<Map>();
            var destination = _mapManager.GetMapById(packet.MapId).As<Map>();
            
            if (source != null)
            {
                source.RemovePlayer(character);
            }
            
            destination.AddPlayer(character);
            
            _pluginManager.Trigger(new MapChangeEvent(client, source, destination));
        }
    }
}