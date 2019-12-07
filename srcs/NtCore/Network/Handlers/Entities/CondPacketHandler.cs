using NtCore.API.Clients;
using NtCore.API.Enums;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class CondPacketHandler : PacketHandler<CondPacket>
    {
        public override void Handle(IClient client, CondPacket packet)
        {
            IMap map = client.Character.Map;

            switch (packet.EntityType)
            {
                case EntityType.MONSTER:
                    map.GetEntity<Monster>(packet.EntityId).Speed = packet.Speed;
                    break;
                case EntityType.NPC:
                    map.GetEntity<Npc>(packet.EntityId).Speed = packet.Speed;
                    break;
                case EntityType.PLAYER:
                    map.GetEntity<Player>(packet.EntityId).Speed = packet.Speed;
                    break;
            }
        }
    }
}