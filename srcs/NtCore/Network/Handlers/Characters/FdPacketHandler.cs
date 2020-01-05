using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class FdPacketHandler : PacketHandler<FdPacket>
    {
        public override void Handle(IClient client, FdPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Reputation = packet.Reputation;
            character.Dignity = packet.Dignity;
        }
    }
}