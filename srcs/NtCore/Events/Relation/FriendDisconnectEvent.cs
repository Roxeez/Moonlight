using NtCore.Clients;
using NtCore.Game.Relation;

namespace NtCore.Events.Relation
{
    public class FriendDisconnectEvent : Event
    {
        public IFriend Friend { get; }
        
        public FriendDisconnectEvent(IClient client, IFriend friend) : base(client)
        {
            Friend = friend;
        }
    }
}