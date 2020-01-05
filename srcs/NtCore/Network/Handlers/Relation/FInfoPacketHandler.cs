using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Relation;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Relation;
using NtCore.Network.Packets.Relation;

namespace NtCore.Network.Handlers.Relation
{
    public class FInfoPacketHandler : PacketHandler<FInfoPacket>
    {
        private readonly IEventManager _eventManager;

        public FInfoPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, FInfoPacket packet)
        {
            Character character = client.Character;

            foreach (FInfoPacket.Info info in packet.Infos)
            {
                Friend friend = character.FindFriendById(info.Id);

                if (friend == null)
                {
                    continue;
                }

                bool wasConnected = friend.IsConnected;

                friend.IsConnected = info.IsConnected;

                if (!wasConnected && friend.IsConnected)
                {
                    _eventManager.CallEvent(new FriendConnectEvent(client, friend));
                }

                if (wasConnected && !friend.IsConnected)
                {
                    _eventManager.CallEvent(new FriendDisconnectEvent(client, friend));
                }
            }
        }
    }
}