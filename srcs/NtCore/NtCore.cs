using NtCore.API;
using NtCore.API.Core;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore
{
    public class NtCore : INtCore
    {
        public IScheduler Scheduler { get; }
        public ILogger Logger { get; }
        public IPluginManager PluginManager { get; }

        public NtCore(IScheduler scheduler, ILogger logger, IPluginManager pluginManager)
        {
            Scheduler = scheduler;
            Logger = logger;
            PluginManager = pluginManager;
        }
    }
}