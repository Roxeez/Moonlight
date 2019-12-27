using System;

namespace NtCore.Game.Relation
{
    public interface IFriend : IEquatable<IFriend>
    {
        int Id { get; }
        string Name { get; }
        bool IsConnected { get; }

        /// <summary>
        ///     Send a private message
        /// </summary>
        /// <param name="message">message to send</param>
        void SendPrivateMessage(string message);

        /// <summary>
        ///     Delete from friends
        /// </summary>
        void Delete();

        /// <summary>
        ///     Join friend miniland
        /// </summary>
        void JoinMiniland();
    }
}