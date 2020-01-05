using NtCore.Clients;
using NtCore.Core;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Game.Entities;
using NtCore.Game.Factory;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class DropPacketHandler : PacketHandler<DropPacket>
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEventManager _eventManager;

        public DropPacketHandler(IEntityFactory entityFactory, IEventManager eventManager)
        {
            _entityFactory = entityFactory;
            _eventManager = eventManager;
        }

        public override void Handle(IClient client, DropPacket packet)
        {
            Map map = client.Character.Map;

            if (map == null)
            {
                return;
            }

            var position = new Position(packet.PositionX, packet.PositionY);
            var owner = map.GetEntity<Player>(packet.OwnerId);

            Drop drop = _entityFactory.CreateDrop(packet.DropId, packet.VNum, packet.Amount, position, owner);

            map.AddEntity(drop);

            _eventManager.CallEvent(new ItemDropEvent(client, drop));
        }
    }
}