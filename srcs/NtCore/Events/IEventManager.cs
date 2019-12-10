using JetBrains.Annotations;
using NtCore.Clients;

namespace NtCore.Events
{
    public interface IEventManager
    {
        /// <summary>
        /// Register your event listener and bind it to this client
        /// Listener events will only be triggered for this client
        /// </summary>
        /// <param name="eventListener">Event listener</param>
        /// <param name="client">Client bind to this listener</param>
        void RegisterEventListener([NotNull] IEventListener eventListener, [NotNull] IClient client);
        
        /// <summary>
        /// Register your event listener
        /// </summary>
        /// <param name="eventListener">Event listener</param>
        void RegisterEventListener([NotNull] IEventListener eventListener);
        
        /// <summary>
        /// Register your event listener and bind it to this client
        /// Listener events will only be triggered for this client
        /// </summary>
        /// <param name="client">Client bind to this listener</param>
        /// <typeparam name="T">type inheriting IEventListener</typeparam>
        void RegisterEventListener<T>([NotNull] IClient client) where T : IEventListener;
        
        /// <summary>
        /// Register your event listener
        /// </summary>
        /// <typeparam name="T">type inheriting IEventListener</typeparam>
        void RegisterEventListener<T>() where T : IEventListener;

        /// <summary>
        /// Trigger all event handler for this event
        /// </summary>
        /// <param name="e">Event to call</param>
        void CallEvent([NotNull] Event e);
    }
}