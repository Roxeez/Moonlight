using System.Collections.Generic;
using JetBrains.Annotations;

namespace NtCore.API.Clients
{
    /// <summary>
    ///     ClientManager is used for managing all clients
    ///     It's useful for managing multiple clientless client
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        ///     Create a local client or return LocalClient if not null
        /// </summary>
        /// <returns>Current LocalClient or new one if null</returns>
        [NotNull]
        IClient CreateLocalClient();
        bool IsLocalCreated { get; }
        IEnumerable<IClient> Clients { get; }
    }
}