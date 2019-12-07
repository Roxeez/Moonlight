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
        /// <param name="listener">Listener to register</param>
        void Register([NotNull] IListener listener, Plugin plugin);

        void Register<T>(Plugin plugin) where T : IListener;

        /// <summary>
        ///     Call an event (execute all registered Handler of this event)
        /// </summary>
        /// <param name="e">Event to call</param>
        void CallEvent([NotNull] Event e);

        /// <summary>
        ///     Start selected plugin
        /// </summary>
        /// <param name="plugin">Plugin to start</param>
        void Start([NotNull] Plugin plugin);
    }
}