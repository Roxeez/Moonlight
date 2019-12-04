using System;
using NtCore.API.Client;
using NtCore.API.Game.Maps;
using NtCore.API.Managers;
using NtCore.Extensions;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly IMapManager _mapManager;
        
        public CMapPacketHandler(IMapManager mapManager)
        {
            _mapManager = mapManager;
        }
        
        public override void Handle(IClient client, CMapPacket packet)
        {
            if (!packet.IsJoining)
            {
                return;
            }

            IMap map = _mapManager.GetMapById(packet.MapId);
            
            map.AsModifiable().AddPlayer(client.Character.AsModifiable());
        }
    }
}