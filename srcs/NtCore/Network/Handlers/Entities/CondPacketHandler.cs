using System;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class CondPacketHandler : PacketHandler<CondPacket>
    {
        public override void Handle(IClient client, CondPacket packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                if (packet.EntityType == EntityType.PLAYER && packet.EntityId == client.Character.Id)
                {
                    client.Character.As<Character>().Speed = packet.Speed;
                }
                return;
            }

            var entity = map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();
            if (entity == null)
            {
                return;
            }

            entity.Speed = packet.Speed;
        }
    }
}