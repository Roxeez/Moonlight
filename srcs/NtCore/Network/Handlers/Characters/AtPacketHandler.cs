using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class AtPacketHandler : PacketHandler<AtPacket>
    {
        public override void Handle(IClient client, AtPacket packet)
        {
            var character = client.Character.As<Character>();
            if (packet.CharacterId != character.Id)
            {
                return;
            }

            if (character.Map.Id != packet.MapId)
            {
                return;
            }
            
            character.Position = new Position(packet.PositionX, packet.PositionY);
        }
    }
}