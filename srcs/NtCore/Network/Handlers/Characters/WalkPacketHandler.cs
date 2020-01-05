using NtCore.Clients;
using NtCore.Core;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class WalkPacketHandler : PacketHandler<WalkPacket>
    {
        private readonly IEventManager _eventManager;

        public WalkPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, WalkPacket packet)
        {
            Character character = client.Character;

            Position from = character.Position;

            character.Speed = packet.Speed;
            character.Position = new Position(packet.X, packet.Y);

            _eventManager.CallEvent(new CharacterMoveEvent(client, from));
        }
    }
}