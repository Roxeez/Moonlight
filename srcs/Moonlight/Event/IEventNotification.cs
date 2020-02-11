using Moonlight.Clients;

namespace Moonlight.Event
{
    public interface IEventNotification
    {
        Client Emitter { get; }
    }
}