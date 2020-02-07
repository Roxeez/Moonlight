using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Game.Entities;
using Moonlight.Packet.Character;

namespace Moonlight.Game.Handlers.Characters
{
    internal class WalkPacketHandler : PacketHandler<WalkPacket>
    {
        protected override void Handle(Client client, WalkPacket packet)
        {
            Character character = client.Character;

            character.Speed = packet.Speed;
            character.Position = new Position(packet.PositionX, packet.PositionY);
        }
    }
}