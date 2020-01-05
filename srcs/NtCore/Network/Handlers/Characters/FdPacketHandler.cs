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
            Character character = client.Character;

            character.Reputation = packet.Reputation;
            character.Dignity = packet.Dignity;
        }
    }
}