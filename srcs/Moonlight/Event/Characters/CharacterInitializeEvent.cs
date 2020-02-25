using Moonlight.Clients;
using Moonlight.Game.Entities;

namespace Moonlight.Event.Characters
{
    public class CharacterInitializeEvent : IEventNotification
    {
        public Client Emitter { get; }
        public Character Character { get; internal set; }
        
        public CharacterInitializeEvent(Client emitter) => Emitter = emitter;
    }
}