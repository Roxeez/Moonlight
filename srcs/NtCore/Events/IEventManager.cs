namespace NtCore.Events
{
    public interface IEventManager
    {
        void RegisterEventListener(IEventListener eventListener);
        void RegisterEventListener<T>() where T : IEventListener;

        void CallEvent(Event e);
    }
}