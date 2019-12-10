using NtCore.Clients;

namespace NtCore.Events
{
    public interface IEventManager
    {
        void RegisterEventListener(IEventListener eventListener, IClient client = null);
        void RegisterEventListener<T>(IClient client = null) where T : IEventListener;

        void CallEvent(Event e);
    }
}