using System.Linq;
using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Relation;
using NtCore.Network.Packets.Relation;

namespace NtCore.Network.Handlers.Relation
{
    public class FInitPacketHandler : PacketHandler<FInitPacket>
    {
        public override void Handle(IClient client, FInitPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Friends = packet.Friends.Select(x => new Friend(client, x.Id, x.Name)
            {
                IsConnected = x.IsConnected
            });
        }
    }
}