using Moonlight.Clients;
using Moonlight.Game.Entities;

namespace Moonlight.Event.Characters
{
    public class StatChangeEvent : IEventNotification
    {
        public Client Emitter { get; }
        public Character Character { get; set; }

        public StatChangeEvent(Client emitter)
        {
            Emitter = emitter;
        }
    }
}