using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class CondPacketHandler : PacketHandler<CondPacket>
    {
        public override void Handle(IClient client, CondPacket packet)
        {
            IMap map = client.Character.Map;
            
            switch (packet.EntityType)
            {
                case EntityType.Monster:
                    map.GetEntity<Monster>(packet.EntityId).Speed = packet.Speed;
                    break;
                case EntityType.Npc:
                    map.GetEntity<Npc>(packet.EntityId).Speed = packet.Speed;
                    break;
                case EntityType.Player:
                    map.GetEntity<Player>(packet.EntityId).Speed = packet.Speed;
                    break;
            }
        }
    }
}