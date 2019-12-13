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
        private readonly ILogger _logger;

        public StPacketHandler(ILogger logger)
        {
            _logger = logger;
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

            character.LastSelectedEntity = entity;
        }
    }
}