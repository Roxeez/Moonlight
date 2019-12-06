using NtCore.API.Client;

namespace NtCore.API.Core
{
    public interface IClientManager
    {
        IClient LocalClient { get; }
        IClient CreateLocalClient();
    }
}