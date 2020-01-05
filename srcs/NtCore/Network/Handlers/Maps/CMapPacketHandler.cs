using System;
using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
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
            Character character = client.Character;
            Map source = client.Character.Map;
            Map destination = _mapManager.GetMapById(packet.MapId);

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
                _eventManager.CallEvent(new MapChangeEvent(client, source, destination));
            }

            character.LastMapChange = DateTime.Now;
        }
    }
}