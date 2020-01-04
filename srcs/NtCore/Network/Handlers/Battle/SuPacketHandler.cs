using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Battle;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
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
            IMap map = client.Character.Map;
            
            if (packet.Damage == 0)
            {
                return;
            }

            var caster = map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();
            var target = map.GetEntity(packet.TargetEntityType, packet.TargetEntityId).As<LivingEntity>();
            if (target == null || caster == null)
            {
                return;
            }

            target.HpPercentage = packet.TargetHpPercentage;

            _eventManager.CallEvent(new EntityDamageEvent(client, target, caster, packet.Damage));
        }
    }
}