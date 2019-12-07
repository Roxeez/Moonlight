using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.Game.Entities
{
    public class Monster : LivingEntity, IMonster
    {
        public Monster()
        {
            EntityType = EntityType.MONSTER;
        }

        public int Vnum { get; set; }
    }
}