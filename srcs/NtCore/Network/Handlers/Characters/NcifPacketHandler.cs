using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class NcifPacketHandler : PacketHandler<NcifPacket>
    {
        private readonly IEventManager _eventManager;

        public NcifPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, NcifPacket packet)
        {
            Character character = client.Character;
            var entity = character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                return;
            }

            Target currentTarget = character.Target;

            character.Target = new Target(entity);

            if (currentTarget != null && !currentTarget.Entity.Equals(entity))
            {
                _eventManager.CallEvent(new TargetChangeEvent(client));
            }
        }
    }
}