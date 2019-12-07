using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Character
{
    public class CharacterMoveEvent : Event
    {
        public ICharacter Character { get; }
        public Position From { get; }

        public CharacterMoveEvent(ICharacter character, Position from)
        {
            Character = character;
            From = from;
        }
    }
}