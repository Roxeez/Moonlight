using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class GetPacketHandler : PacketHandler<GetPacket>
    {
        private readonly IEventManager _eventManager;

        public GetPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, GetPacket packet)
        {
            Map map = client.Character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            Drop drop = map.GetEntity<Drop>(packet.DropId);

            if (entity == null && drop == null)
            {
                return;
            }

            map.RemoveEntity(drop);

            _eventManager.CallEvent(new EntityPickupDropEvent(client, entity, drop));
        }
    }
}