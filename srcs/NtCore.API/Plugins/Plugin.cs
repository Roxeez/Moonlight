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

        private ILogger _logger;
        public ILogger Logger => _logger ?? (_logger = new ConsoleLogger($"NtCore - {Name}"));

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