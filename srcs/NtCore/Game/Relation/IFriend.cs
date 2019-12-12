using System;

namespace NtCore.Game.Relation
{
    public interface IFriend : IEquatable<IFriend>
    {
        int Id { get; }
        string Name { get; }
        bool IsConnected { get; }

        void SendPrivateMessage(string message);
    }
}