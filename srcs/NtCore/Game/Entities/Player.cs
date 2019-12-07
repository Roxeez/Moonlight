using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.Game.Entities
{
    public class Player : LivingEntity, IPlayer
    {
        public Player() => EntityType = EntityType.PLAYER;

        public string Name { get; set; }
        public int Reputation { get; set; }
        public int Dignity { get; set; }
        public ClassType Class { get; set; }
        public Gender Gender { get; set; }
        public Faction Faction { get; set; }
    }
}