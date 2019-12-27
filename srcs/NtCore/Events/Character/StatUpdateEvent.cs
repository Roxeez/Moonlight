using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Character
{
    public class StatUpdateEvent : Event
    {
        public StatUpdateEvent(IClient client) : base(client) => Character = client.Character;

        public ICharacter Character { get; }
    }
}