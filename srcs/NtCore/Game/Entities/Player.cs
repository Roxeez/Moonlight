using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Player : LivingEntity
    {
        public Player() => EntityType = EntityType.PLAYER;

        /// <summary>
        /// Reputation of player
        /// </summary>
        public int Reputation { get; internal set; }
        
        /// <summary>
        /// Dignity of player
        /// </summary>
        public int Dignity { get; internal set; }
        
        /// <summary>
        /// Class of player
        /// </summary>
        public ClassType Class { get; internal set; }
        
        /// <summary>
        /// Gender of player
        /// </summary>
        public Gender Gender { get; internal set; }
        
        /// <summary>
        /// Faction of player
        /// </summary>
        public Faction Faction { get; internal set; }
    }
}