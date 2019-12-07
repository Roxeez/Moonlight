using NtCore.API.Commands;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore.API
{
    public interface INtCore
    {
        IScheduler Scheduler { get; }
        ILogger Logger { get; }
        IPluginManager PluginManager { get; }
        ICommandManager CommandManager { get; }
    }
}