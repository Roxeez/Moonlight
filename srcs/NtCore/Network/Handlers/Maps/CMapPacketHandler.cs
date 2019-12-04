using NtCore.API.Client;
using NtCore.API.Events.Maps;
using NtCore.API.Game.Maps;
using NtCore.API.Managers;
using NtCore.Extensions;
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

            IMap oldMap = client.Character.Map;
            if (oldMap != null)
            {
                var currentMap = oldMap.AsModifiable();
                currentMap.RemovePlayer(client.Character.AsModifiable());
            }

            IMap map = _mapManager.GetMapById(packet.MapId);
            map.AsModifiable().AddPlayer(client.Character.AsModifiable());
            
            _pluginManager.Trigger(new MapChangeEvent(client, oldMap, map));
        }
    }
}