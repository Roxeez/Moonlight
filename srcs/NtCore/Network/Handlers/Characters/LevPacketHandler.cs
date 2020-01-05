using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class LevPacketHandler : PacketHandler<LevPacket>
    {
        public override void Handle(IClient client, LevPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Level = packet.Level;
            character.JobLevel = packet.JobLevel;
        }
    }
}