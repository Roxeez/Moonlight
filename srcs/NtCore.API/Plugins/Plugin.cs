using NtCore.API.Client;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore.API.Plugins
{
    public abstract class Plugin
    {
        private readonly IPluginManager _pluginManager;

        public IScheduler Scheduler { get; }
        public ILogger Logger { get; }

        protected Plugin(IPluginManager pluginManager, IScheduler scheduler, ILogger logger)
        {
            _pluginManager = pluginManager;

            Scheduler = scheduler;
            Logger = logger;
        }

        public abstract void Run();

        public void RegisterListeners(params IListener[] listeners)
        {
            _pluginManager.Register(listeners);
        }
    }
}