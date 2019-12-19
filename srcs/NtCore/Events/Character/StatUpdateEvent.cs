using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Character
{
    public class StatUpdateEvent : Event
    {
        public ICharacter Character { get; }
        
        public StatUpdateEvent(IClient client) : base(client)
        {
            Character = client.Character;
        }
    }
}