using NtCore.Enums;

namespace NtCore.Game.Entities.Impl
{
    public class Player : LivingEntity, IPlayer
    {
        public Player() => EntityType = EntityType.PLAYER;

        public int Reputation { get; set; }
        public int Dignity { get; set; }
        public ClassType Class { get; set; }
        public Gender Gender { get; set; }
        public Faction Faction { get; set; }
    }
}