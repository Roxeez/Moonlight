namespace NtCore.Events
{
    public interface IEventManager
    {
        void Register(IListener listener);
        void Register<T>() where T : IListener;

        void CallEvent(Event e);
    }
}