using NtCore.API.Logger;

namespace NtCore.API.Plugins
{
    public abstract class Plugin
    {
        private ILogger _logger;
        public abstract string Name { get; }
        public abstract string Version { get; }
        public ILogger Logger => _logger ?? (_logger = new ConsoleLogger($"NtCore - {Name}"));

        /// <summary>
        ///     Called on plugin start
        /// </summary>
        public virtual void OnEnable()
        {
        }

        /// <summary>
        ///     Called on plugin shutdown
        /// </summary>
        public virtual void OnDisable()
        {
        }
    }
}