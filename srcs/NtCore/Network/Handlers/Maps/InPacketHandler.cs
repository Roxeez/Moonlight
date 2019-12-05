using System;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Events.Maps;
using NtCore.API.Game.Entities;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class InPacketHandler : PacketHandler<InPacket>
    {
        private readonly PluginManager _pluginManager;

        public InPacketHandler(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }
        
        public override void Handle(IClient client, InPacket packet)
        {
            var map = client.Character.Map.As<Map>();
            if (map == null)
            {
                return;
            }

            IEntity entity;
            switch (packet.EntityType)
            {
                case EntityType.Npc:
                    entity = new Npc
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
                    break;
                case EntityType.Monster:
                    entity = new Monster
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
                    break;
                case EntityType.Drop:
                    entity = new Drop
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        Amount = packet.Amount
                    };
                    break;
                case EntityType.Player:
                    entity = new Player
                    {
                        Id = packet.Id,
                        Name = packet.Name,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            map.AddEntity(entity);
            
            _pluginManager.CallEvent(new EntitySpawnEvent(client, entity, map));
        }
    }
}