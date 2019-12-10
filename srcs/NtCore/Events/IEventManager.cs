using NtCore.Clients;

namespace NtCore.Events
{
    public interface IEventManager
    {
        void RegisterEventListener(IEventListener eventListener, IClient client);
        void RegisterEventListener(IEventListener eventListener);
        void RegisterEventListener<T>(IClient client) where T : IEventListener;
        void RegisterEventListener<T>() where T : IEventListener;

        void CallEvent(Event e);
    }
}