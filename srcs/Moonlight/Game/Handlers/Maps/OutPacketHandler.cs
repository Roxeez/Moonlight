using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Game.Handlers.Maps
{
    internal class OutPacketHandler : PacketHandler<OutPacket>
    {
        private readonly ILogger _logger;

        public OutPacketHandler(ILogger logger) => _logger = logger;

        protected override void Handle(Client client, OutPacket packet)
        {
            Map map = client.Character.Map;

            if (map == null)
            {
                _logger.Warn("Handling OutPacket but character map is null");
                return;
            }

            map.RemoveEntity(packet.EntityType, packet.EntityId);
            _logger.Info($"Entity {packet.EntityType} with id {packet.EntityId} removed from map {map.Id}");
        }
    }
}