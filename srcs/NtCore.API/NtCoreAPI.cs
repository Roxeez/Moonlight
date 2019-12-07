using NtCore.API.Core;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore.API
{
    public static class NtCoreAPI
    {
        public static IScheduler Scheduler { get; private set; }
        public static ILogger Logger { get; private set; }
        public static IPluginManager PluginManager { get; private set; }

        public static void Initialize(IScheduler scheduler, IPluginManager pluginManager, ILogger logger)
        {
            Scheduler = scheduler;
            PluginManager = pluginManager;
            Logger = logger;
        }
    }
}