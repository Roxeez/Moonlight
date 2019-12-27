using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps.Impl;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class GetPacketHandler : PacketHandler<GetPacket>
    {
        private readonly IEventManager _eventManager;

        public GetPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, GetPacket packet)
        {
            var map = client.Character.Map.As<Map>();

            var entity = map.GetEntity(packet.EntityType, packet.EntityId).As<ILivingEntity>();
            var drop = map.GetEntity<IDrop>(packet.DropId);

            if (entity == null && drop == null)
            {
                return;
            }

            map.RemoveEntity(drop);

            _eventManager.CallEvent(new EntityPickupDropEvent(client, entity, drop));
        }
    }
}