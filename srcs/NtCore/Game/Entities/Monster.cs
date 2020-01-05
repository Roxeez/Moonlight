using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Monster : LivingEntity
    {
        public Monster() => EntityType = EntityType.MONSTER;

        public int Vnum { get; internal set; }
    }
}