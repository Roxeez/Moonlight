using NtCore.Clients;
using NtCore.Game.Relation;

namespace NtCore.Events.Relation
{
    public class FriendDisconnectEvent : Event
    {
        public IFriend Friend { get; }
        
        public FriendConnectEvent(IClient client, IFriend friend) : base(client)
        {
            Friend = friend;
        }
    }
}