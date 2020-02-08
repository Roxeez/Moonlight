using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Game.Handlers.Maps
{
    internal class GetPacketHandler : PacketHandler<GetPacket>
    {
        protected override void Handle(Client client, GetPacket packet)
        {
            Map map = client.Character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            Drop drop = map.GetEntity<Drop>(packet.DropId);

            if (entity == null && drop == null)
            {
                return;
            }

            map.RemoveEntity(drop);
        }
    }
}