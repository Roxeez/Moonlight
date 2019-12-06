using NtCore.API.Client;
using NtCore.API.Core;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore.API.Plugins
{
    public abstract class Plugin
    {
        private readonly IPluginManager _pluginManager;

        public IScheduler Scheduler { get; }
        public ILogger Logger { get; }
        public IClientManager ClientManager { get; }

        protected Plugin(IPluginManager pluginManager, IScheduler scheduler, ILogger logger, IClientManager clientManager)
        {
            _pluginManager = pluginManager;

            ClientManager = clientManager;
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