using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Player : LivingEntity
    {
        public Player() => EntityType = EntityType.PLAYER;

        public int Reputation { get; internal set; }
        public int Dignity { get; internal set; }
        public ClassType Class { get; internal set; }
        public Gender Gender { get; internal set; }
        public Faction Faction { get; internal set; }
    }
}