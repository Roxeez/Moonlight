using System;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Core;
using NtCore.API.Events.Maps;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Managers;
using NtCore.API.Scheduler;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly ILogger _logger;
        private readonly IPluginManager _pluginManager;
        private readonly IMapManager _mapManager;
        private readonly IScheduler _scheduler;
        
        public CMapPacketHandler(ILogger logger, IMapManager mapManager, IPluginManager pluginManager, IScheduler scheduler)
        {
            _logger = logger;
            _pluginManager = pluginManager;
            _mapManager = mapManager;
            _scheduler = scheduler;
        }
        
        public override void Handle(IClient client, CMapPacket packet)
        {
            var character = client.Character.As<Character>();
            var source = client.Character.Map.As<Map>();
            var destination = _mapManager.GetMapById(packet.MapId).As<Map>();

            if (!packet.IsJoining)
            {
                _logger.Debug("Is not joining map");
                return;
            }
            
            if (source != null)
            {
                source.RemoveEntity(character);
            }
            
            destination.AddEntity(character);

            if (source != null)
            {
                _pluginManager.CallEvent(new MapChangeEvent(client, source, destination));
            }

            character.LastMapChange = DateTime.Now;
        }
    }
}