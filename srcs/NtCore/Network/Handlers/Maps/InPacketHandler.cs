using System;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Events;
using NtCore.Events.Entity;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class InPacketHandler : PacketHandler<InPacket>
    {
        private readonly IEventManager _eventManager;

        public InPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

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
                    entity = new Npc
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        Direction = packet.Direction,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
                    break;
                case EntityType.MONSTER:
                    entity = new Monster
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        Direction = packet.Direction,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
                    break;
                case EntityType.DROP:
                    entity = new Drop
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        Amount = packet.Amount
                    };
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

            if (character.LastMapChange.AddSeconds(5) < DateTime.Now)
            {
                _eventManager.CallEvent(new EntityJoinEvent(client, entity, map));
            }
        }
    }
}