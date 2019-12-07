using NtCore.API.Core;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore.API
{
    public interface INtCore
    {
        IScheduler Scheduler { get; }
        ILogger Logger { get; }
        IPluginManager PluginManager { get; }
    }
}