using System.Reflection;
using NtCore.API.Core;
using NtCore.API.Logger;
using NtCore.API.Scheduler;

namespace NtCore.API.Plugins
{
    public abstract class Plugin
    {
        public abstract string Name { get; }
        public abstract string Version { get; }
        
        public IPluginManager PluginManager { get; }
        public IScheduler Scheduler { get; }
        
        private ILogger _logger;
        public ILogger Logger => _logger ?? (_logger = new ConsoleLogger(Name));

        protected Plugin()
        {
            Scheduler = NtCoreAPI.Scheduler;
            PluginManager = NtCoreAPI.PluginManager;
        }

        /// <summary>
        /// Called on plugin start
        /// </summary>
        public virtual void OnEnable()
        {
            
        }

        /// <summary>
        /// Called on plugin shutdown
        /// </summary>
        public virtual void OnDisable()
        {
            
        }
    }
}