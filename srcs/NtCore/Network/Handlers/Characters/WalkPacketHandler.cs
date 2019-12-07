using NtCore.API;
using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class WalkPacketHandler : PacketHandler<WalkPacket>
    {
        public override void Handle(IClient client, WalkPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Speed = packet.Speed;
            character.Position = new Position(packet.X, packet.Y);
        }
    }
}