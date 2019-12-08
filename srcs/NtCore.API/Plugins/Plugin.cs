using NtCore.API.Logger;

namespace NtCore.API.Plugins
{
    /// <summary>
    ///     All plugin main class should implement this class
    /// </summary>
    public abstract class Plugin
    {
        private ILogger _logger;

        /// <summary>
        ///     Plugin name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///     Plugin version
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        ///     Plugin logger
        /// </summary>
        public ILogger Logger => _logger ?? (_logger = new ConsoleLogger($"{Name}"));

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