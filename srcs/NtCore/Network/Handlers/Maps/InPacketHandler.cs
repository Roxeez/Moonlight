using System;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Events;
using NtCore.Events.Entity;
using NtCore.Game.Entities;
using NtCore.Game.Factory;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class InPacketHandler : PacketHandler<InPacket>
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEventManager _eventManager;

        public InPacketHandler(IEventManager eventManager, IEntityFactory entityFactory)
        {
            _eventManager = eventManager;
            _entityFactory = entityFactory;
        }

        public override void Handle(IClient client, InPacket packet)
        {
            Character character = client.Character;
            Map map = client.Character.Map;

            if (map == null)
            {
                return;
            }

            if (map.GetEntity(packet.EntityType, packet.Id) != null)
            {
                return;
            }

            Entity entity;
            switch (packet.EntityType)
            {
                case EntityType.NPC:
                    entity = _entityFactory.CreateNpc(packet.Id, packet.Vnum, packet.Position, packet.Direction, packet.HpPercentage, packet.MpPercentage);
                    break;
                case EntityType.MONSTER:
                    entity = _entityFactory.CreateMonster(packet.Id, packet.Vnum, packet.Position, packet.Direction, packet.HpPercentage, packet.MpPercentage);
                    break;
                case EntityType.DROP:
                    var owner = map.GetEntity<Player>(packet.DropOwnerId);
                    entity = _entityFactory.CreateDrop(packet.Id, packet.Vnum, packet.Amount, packet.Position, owner);
                    break;
                case EntityType.PLAYER:
                    entity = _entityFactory.CreatePlayer(packet.Id, packet.Name, packet.Level, packet.ClassType, packet.Direction, packet.Gender, packet.Position, packet.HpPercentage,
                        packet.MpPercentage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            map.AddEntity(entity);

            if (character.LastMapChange.AddSeconds(5) < DateTime.Now && entity is LivingEntity livingEntity)
            {
                _eventManager.CallEvent(new EntityJoinEvent(client, livingEntity, map));
            }
        }
    }
}