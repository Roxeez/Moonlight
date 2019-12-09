using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Extensions;
using NtCore.Game.Battle.Impl;
using NtCore.Game.Entities.Impl;
using NtCore.Logger;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class StPacketHandler : PacketHandler<StPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public StPacketHandler(ILogger logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }

        public override void Handle(IClient client, StPacket packet)
        {
            var character = client.Character.As<Character>();
            var entity = character.Map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();

            if (entity == null)
            {
                _logger.Warning($"{nameof(StPacketHandler)} - Can't find entity in map.");
                return;
            }

            entity.HpPercentage = packet.HpPercentage;
            entity.MpPercentage = packet.MpPercentage;
            entity.Level = packet.Level;

            if (character.Target == null || !character.Target.Entity.Equals(entity))
            {
                character.Target = new Target(entity)
                {
                    Hp = packet.Hp,
                    Mp = packet.Mp
                };

                _eventManager.CallEvent(new TargetChangeEvent(client.Character));
                return;
            }

            var target = character.Target.As<Target>();

            target.Hp = packet.Hp;
            target.Mp = packet.Mp;

            _eventManager.CallEvent(new TargetStatUpdateEvent(client.Character));
        }
    }
}