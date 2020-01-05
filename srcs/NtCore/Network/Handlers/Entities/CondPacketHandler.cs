using NtCore.Clients;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class CondPacketHandler : PacketHandler<CondPacket>
    {
        public override void Handle(IClient client, CondPacket packet)
        {
            Map map = client.Character.Map;
            if (map == null)
            {
                if (packet.EntityType == EntityType.PLAYER && packet.EntityId == client.Character.Id)
                {
                    client.Character.Speed = packet.Speed;
                }

                return;
            }

            var entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }

            entity.Speed = packet.Speed;
        }
    }
}