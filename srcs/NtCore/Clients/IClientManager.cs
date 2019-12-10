using System.Collections.Generic;
using JetBrains.Annotations;

namespace NtCore.Clients
{
    /// <summary>
    ///     ClientManager is used for managing all clients
    ///     It's useful for managing multiple clientless client
    /// </summary>
    public interface IClientManager
    {
        IClient LocalClient { get; }
        /// <summary>
        ///     Contains all existing clients
        /// </summary>
        IEnumerable<IClient> Clients { get; }

        /// <summary>
        ///     Create a local client
        /// </summary>
        /// <returns>Current LocalClient or new one if null</returns>
        [NotNull]
        IClient CreateLocalClient();

        [NotNull]
        IClient CreateRemoteClient();
    }
}