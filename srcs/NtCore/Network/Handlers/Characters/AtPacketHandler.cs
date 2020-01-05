using NtCore.Clients;
using NtCore.Core;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class AtPacketHandler : PacketHandler<AtPacket>
    {
        private readonly IMapManager _mapManager;

        public AtPacketHandler(IMapManager mapManager) => _mapManager = mapManager;

        public override void Handle(IClient client, AtPacket packet)
        {
            Character character = client.Character;
            if (packet.CharacterId != character.Id)
            {
                return;
            }

            character.Map = _mapManager.GetMapById(packet.MapId);
            character.Position = new Position(packet.PositionX, packet.PositionY);
        }
    }
}