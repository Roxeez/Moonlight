﻿using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Battle;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Battle;

namespace NtCore.Network.Handlers.Battle
{
    public class SuPacketHandler : PacketHandler<SuPacket>
    {
        private readonly IEventManager _eventManager;

        public SuPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        public override void Handle(IClient client, SuPacket packet)
        {
            Map map = client.Character.Map;
            
            if (packet.Damage == 0)
            {
                return;
            }

            var caster = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            var target = map.GetEntity<LivingEntity>(packet.TargetEntityType, packet.TargetEntityId);
            if (target == null || caster == null)
            {
                return;
            }

            target.HpPercentage = packet.TargetHpPercentage;

            _eventManager.CallEvent(new EntityDamageEvent(client, target, caster, packet.Damage));
        }
    }
}