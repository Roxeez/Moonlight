using System;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Events;
using NtCore.Events.Entity;
using NtCore.Extensions;
using NtCore.Factory;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class InPacketHandler : PacketHandler<InPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly IEntityFactory _entityFactory;

        public InPacketHandler(IEventManager eventManager, IEntityFactory entityFactory)
        {
            _eventManager = eventManager;
            _entityFactory = entityFactory;
        }

        public override void Handle(IClient client, InPacket packet)
        {
            var character = client.Character.As<Character>();
            var map = client.Character.Map.As<Map>();

            if (map == null)
            {
                return;
            }

            IEntity entity;
            switch (packet.EntityType)
            {
                case EntityType.NPC:
                    Npc npc = _entityFactory.CreateNpc(packet.Vnum);
                    npc.Id = packet.Id;
                    npc.Position = packet.Position;
                    npc.Direction = packet.Direction;
                    npc.HpPercentage = packet.HpPercentage;
                    npc.MpPercentage = packet.MpPercentage;
                    entity = npc;
                    break;
                case EntityType.MONSTER:
                    Monster monster = _entityFactory.CreateMonster(packet.Vnum);
                    monster.Id = packet.Id;
                    monster.Position = packet.Position;
                    monster.Direction = packet.Direction;
                    monster.HpPercentage = packet.HpPercentage;
                    monster.MpPercentage = packet.MpPercentage;
                    entity = monster;
                    break;
                case EntityType.DROP:
                    Drop drop = _entityFactory.CreateDrop(packet.Vnum);
                    drop.Id = packet.Id;
                    drop.Position = packet.Position;
                    drop.Amount = packet.Amount;
                    entity = drop;
                    break;
                case EntityType.PLAYER:
                    entity = new Player
                    {
                        Id = packet.Id,
                        Name = packet.Name,
                        Level = packet.Level,
                        Class = packet.ClassType,
                        Direction = packet.Direction,
                        Gender = packet.Gender,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
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