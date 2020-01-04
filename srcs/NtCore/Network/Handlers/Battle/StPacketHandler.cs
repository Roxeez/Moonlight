using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Battle;
using NtCore.Events.Character;
using NtCore.Extensions;
using NtCore.Game.Battle.Impl;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Battle;

namespace NtCore.Network.Handlers.Battle
{
    public class StPacketHandler : PacketHandler<StPacket>
    {
        private readonly IEventManager _eventManager;

        public StPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        public override void Handle(IClient client, StPacket packet)
        {
            var character = client.Character.As<Character>();

            var targetEntity = character.Map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();
            if (targetEntity == null)
            {
                return;
            }

            targetEntity.HpPercentage = packet.HpPercentage;
            targetEntity.MpPercentage = packet.MpPercentage;

            var currentTarget = character.Target.As<Target>();
            if (currentTarget != null && currentTarget.Entity.Equals(targetEntity))
            {
                currentTarget.Hp = packet.CurrentHp;
                currentTarget.Mp = packet.CurrentMp;

                return;
            }

            character.Target = new Target(targetEntity)
            {
                Hp = packet.CurrentHp,
                Mp = packet.CurrentMp
            };
            
            _eventManager.CallEvent(new TargetChangeEvent(client));
        }
    }
}