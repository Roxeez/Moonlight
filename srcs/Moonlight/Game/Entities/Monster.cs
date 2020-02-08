using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent any kind of Monster
    /// </summary>
    public class Monster : LivingEntity
    {
        private readonly MonsterDto _monsterDto;

        internal Monster(long id, MonsterDto monsterDto, string name) : base(id, name, EntityType.MONSTER) => _monsterDto = monsterDto;

        public int Vnum => _monsterDto.Id;
    }
}