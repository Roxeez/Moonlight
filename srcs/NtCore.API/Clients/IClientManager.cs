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
        ///     Get the current local client
        /// </summary>
        [CanBeNull]
        IClient LocalClient { get; }

        /// <summary>
        ///     Create a local client or return LocalClient if not null
        /// </summary>
        /// <returns>Current LocalClient or new one if null</returns>
        [NotNull]
        IClient CreateLocalClient();
    }
}