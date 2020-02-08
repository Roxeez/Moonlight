using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Game.Entities;
using Moonlight.Packet.Entity;

namespace Moonlight.Game.Handlers.Entities
{
    internal class MvPacketHandler : PacketHandler<MvPacket>
    {
        protected override void Handle(Client client, MvPacket packet)
        {
            LivingEntity entity = client.Character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                return;
            }

            entity.Position = new Position(packet.PositionX, packet.PositionY);
            entity.Speed = packet.Speed;
        }
    }
}