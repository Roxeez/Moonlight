using NtCore.API.Client;
using NtCore.API.Logger;

namespace NtCore.API
{
    public interface INtCore
    {
        IPluginManager PluginManager { get; }
        IClient CreateLocalClient();
        IClient CreateRemoteClient();
    }
}