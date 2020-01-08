﻿using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Relation;

namespace NtCore.Events.Relation
{
    /// <summary>
    /// Event called when a friend disconnect
    /// </summary>
    public class FriendDisconnectEvent : Event
    {
        public FriendDisconnectEvent(IClient client, Friend friend) : base(client) => Friend = friend;

        /// <summary>
        /// Friend involved in this event
        /// </summary>
        [NotNull]
        public Friend Friend { get; }
    }
}