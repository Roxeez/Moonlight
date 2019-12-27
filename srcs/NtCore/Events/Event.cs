using NtCore.Clients;

namespace NtCore.Events
{
    /// <summary>
    ///     Represent an Event handled by PluginManager
    /// </summary>
    public abstract class Event
    {
        protected Event(IClient client) => Client = client;

        /// <summary>
        ///     Client triggering this event
        /// </summary>
        public IClient Client { get; }
    }
}