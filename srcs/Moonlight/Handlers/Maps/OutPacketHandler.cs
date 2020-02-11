using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.Maps;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Handlers.Maps
{
    internal class OutPacketHandler : PacketHandler<OutPacket>
    {
        private readonly ILogger _logger;
        private readonly IEventManager _eventManager;

        public OutPacketHandler(ILogger logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, OutPacket packet)
        {
            Map map = client.Character.Map;

            if (map == null)
            {
                _logger.Warn("Handling OutPacket but character map is null");
                return;
            }

            Entity entity = map.GetEntity(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }

            map.RemoveEntity(packet.EntityType, packet.EntityId);
            
            _eventManager.Emit(new EntityLeaveEvent
            {
                Map = map,
                Entity = entity,
            });
        }
    }
}