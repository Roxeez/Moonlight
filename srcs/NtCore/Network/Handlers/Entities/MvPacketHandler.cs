using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Events.Entity;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class MvPacketHandler : PacketHandler<MvPacket>
    {
        private readonly IEventManager _eventManager;

        public MvPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, MvPacket packet)
        {
            ICharacter character = client.Character;
            var entity = client.Character.Map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();

            if (entity == null)
            {
                return;
            }

            Position from = character.Position;

            entity.Position = new Position(packet.X, packet.Y);
            entity.Speed = packet.Speed;

            _eventManager.CallEvent(new EntityMoveEvent(client, entity, from));

            if (character.Target != null && character.Target.Entity.Equals(entity))
            {
                _eventManager.CallEvent(new TargetMoveEvent(client, from));
            }
        }
    }
}