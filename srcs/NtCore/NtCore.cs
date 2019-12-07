using NtCore.API;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore
{
    public class NtCore : INtCore
    {
        public NtCore(IScheduler scheduler, ILogger logger, IPluginManager pluginManager)
        {
            Scheduler = scheduler;
            Logger = logger;
            PluginManager = pluginManager;
        }

        public IScheduler Scheduler { get; }
        public ILogger Logger { get; }
        public IPluginManager PluginManager { get; }
    }
}