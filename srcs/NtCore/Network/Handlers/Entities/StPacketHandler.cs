using NtCore.API.Clients;
using NtCore.API.Events.Character;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class StPacketHandler : PacketHandler<StPacket>
    {
        private readonly ILogger _logger;
        private readonly IPluginManager _pluginManager;

        public StPacketHandler(ILogger logger, IPluginManager pluginManager)
        {
            _logger = logger;
            _pluginManager = pluginManager;
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

                _pluginManager.CallEvent(new TargetChangeEvent(client.Character));
                return;
            }

            var target = character.Target.As<Target>();

            target.Hp = packet.Hp;
            target.Mp = packet.Mp;

            _pluginManager.CallEvent(new TargetStatUpdateEvent(client.Character));
        }
    }
}