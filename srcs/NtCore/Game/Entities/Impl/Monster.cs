using NtCore.Enums;

namespace NtCore.Game.Entities.Impl
{
    public class Monster : LivingEntity, IMonster
    {
        public Monster() => EntityType = EntityType.MONSTER;

        public int Vnum { get; set; }
    }
}