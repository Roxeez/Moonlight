using NtCore.API;
using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class MvPacketHandler : PacketHandler<MvPacket>
    {
        public override void Handle(IClient client, MvPacket packet)
        {
            var entity = client.Character.Map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();

            if (entity == null)
            {
                return;
            }
            
            entity.Position = new Position(packet.X, packet.Y);
            entity.Speed = packet.Speed;
        }
    }
}