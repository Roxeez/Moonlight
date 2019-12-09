using System;
using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly IMapManager _mapManager;

        public CMapPacketHandler(IMapManager mapManager, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _mapManager = mapManager;
        }

        public override void Handle(IClient client, CMapPacket packet)
        {
            var character = client.Character.As<Character>();
            var source = client.Character.Map.As<Map>();
            var destination = _mapManager.GetMapById(packet.MapId).As<Map>();

            if (!packet.IsJoining)
            {
                return;
            }

            if (source != null)
            {
                source.RemoveEntity(character);
            }

            destination.AddEntity(character);

            if (source != null)
            {
                _eventManager.CallEvent(new MapChangeEvent(client.Character, source, destination));
            }

            character.LastMapChange = DateTime.Now;
        }
    }
}