using JetBrains.Annotations;

namespace NtCore.API.Plugins
{
    /// <summary>
    ///     PluginManager used for registering listener & call events
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        ///     Register selected IListener
        /// </summary>
        /// <param name="plugin">Plugin instance</param>
        /// <param name="listeners">Listeners to register</param>
        void RegisterListeners([NotNull] Plugin plugin, [NotNull] params IListener[] listeners);

        /// <summary>
        ///     Call an event (execute all registered Handler of this event)
        /// </summary>
        /// <param name="e">Event to call</param>
        void CallEvent([NotNull] Event e);

        /// <summary>
        /// Start selected plugin
        /// </summary>
        /// <param name="plugin">Plugin to start</param>
        void Start([NotNull] Plugin plugin);
    }
}