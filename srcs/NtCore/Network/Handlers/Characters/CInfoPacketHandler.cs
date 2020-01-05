using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class CInfoPacketHandler : PacketHandler<CInfoPacket>
    {
        public override void Handle(IClient client, CInfoPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Id = packet.CharacterId;
            character.Class = packet.Class;
            character.Gender = packet.Gender;
            character.Name = packet.Name;
        }
    }
}