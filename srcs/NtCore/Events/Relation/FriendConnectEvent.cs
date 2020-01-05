using NtCore.Clients;
using NtCore.Game.Relation;

namespace NtCore.Events.Relation
{
    public class FriendConnectEvent : Event
    {
        public FriendConnectEvent(IClient client, Friend friend) : base(client) => Friend = friend;

        public Friend Friend { get; }
    }
}