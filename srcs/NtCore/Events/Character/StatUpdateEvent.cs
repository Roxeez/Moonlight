using NtCore.Clients;

namespace NtCore.Events.Character
{
    public class StatUpdateEvent : Event
    {
        public StatUpdateEvent(IClient client) : base(client) => Character = client.Character;

        public Game.Entities.Character Character { get; }
    }
}