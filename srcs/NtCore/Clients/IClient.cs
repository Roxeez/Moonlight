using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NtCore.Enums;
using NtCore.Game.Entities;

namespace NtCore.Clients
{
    /// <summary>
    ///     Represent a Client
    ///     Client can be a LocalClient (injected) or RemoteClient (clientless)
    /// </summary>
    public interface IClient : IDisposable, IEquatable<IClient>
    {
        /// <summary>
        ///     Client Unique Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     Get the character of this session
        /// </summary>
        Character Character { get; }

        /// <summary>
        ///     Client type
        /// </summary>
        ClientType Type { get; }

        /// <summary>
        ///     Send a packet
        /// </summary>
        /// <param name="packet">Packet to send</param>
        Task SendPacket([NotNull] string packet);

        /// <summary>
        ///     Receive a packet
        /// </summary>
        /// <param name="packet">Packet to receive</param>
        Task ReceivePacket([NotNull] string packet);

        event Func<string, bool> PacketSend;
        event Func<string, bool> PacketReceived;
    }
}