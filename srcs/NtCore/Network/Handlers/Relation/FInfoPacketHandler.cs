using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Relation.Impl;
using NtCore.Network.Packets.Relation;

namespace NtCore.Network.Handlers.Relation
{
    public class FInfoPacketHandler : PacketHandler<FInfoPacket>
    {
        public override void Handle(IClient client, FInfoPacket packet)
        {
            var character = client.Character.As<Character>();
            
            packet.Infos.ForEach(x =>
            {
                var friend = character.GetFriendById(x.Id).As<Friend>();

                friend.IsConnected = x.IsConnected;
            });
        }
    }
}