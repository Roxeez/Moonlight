using System;
using JetBrains.Annotations;
using NtCore.API.Game.Entities;

namespace NtCore.API.Client
{
    /// <summary>
    /// Represent a Client
    /// Client can be a LocalClient (injected) or RemoteClient (clientless)
    /// </summary>
    public interface IClient : IDisposable
    {
        /// <summary>
        /// Get the character of this session
        /// </summary>
        ICharacter Character { get; }

        /// <summary>
        /// Send a packet
        /// </summary>
        /// <param name="packet">Packet to send</param>
        void SendPacket([NotNull] string packet);
        
        /// <summary>
        /// Receive a packet
        /// </summary>
        /// <param name="packet">Packet to receive</param>
        void ReceivePacket([NotNull] string packet);
    }
}