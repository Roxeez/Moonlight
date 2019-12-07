using NtCore.API.Game.Battle;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Character
{
    public class TargetMoveEvent : Event
    {
        public ICharacter Character { get; }
        public Position From { get; }

        public TargetMoveEvent(ICharacter character, Position from)
        {
            Character = character;
            From = from;
        }
    }
}