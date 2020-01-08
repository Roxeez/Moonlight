using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Monster : LivingEntity
    {
        public Monster() => EntityType = EntityType.MONSTER;

        /// <summary>
        /// Vnum of the monster
        /// </summary>
        public int Vnum { get; internal set; }
    }
}