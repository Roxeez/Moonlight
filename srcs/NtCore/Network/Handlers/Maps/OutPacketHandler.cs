using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Entity;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class OutPacketHandler : PacketHandler<OutPacket>
    {
        private readonly IEventManager _eventManager;

        public OutPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, OutPacket packet)
        {
            Map map = client.Character.Map;
            Entity entity = map.GetEntity(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                return;
            }

            map.RemoveEntity(entity);

            if (entity is LivingEntity livingEntity)
            {
                _eventManager.CallEvent(new EntityLeaveEvent(client, livingEntity, map));
            }
        }
    }
}