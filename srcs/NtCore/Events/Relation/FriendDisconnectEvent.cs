using NtCore.Clients;
using NtCore.Game.Relation;

namespace NtCore.Events.Relation
{
    public class FriendDisconnectEvent : Event
    {
        public FriendDisconnectEvent(IClient client, IFriend friend) : base(client) => Friend = friend;

        public IFriend Friend { get; }
    }
}