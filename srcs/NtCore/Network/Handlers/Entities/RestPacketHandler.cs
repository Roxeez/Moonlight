using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Entity;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class RestPacketHandler : PacketHandler<RestPacket>
    {
        private readonly IEventManager _eventManager;

        public RestPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        public override void Handle(IClient client, RestPacket packet)
        {
            LivingEntity entity = client.Character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                return;
            }

            entity.IsResting = packet.IsResting;
            
            _eventManager.CallEvent(new EntityRestEvent(client, entity));
        }
    }
}