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

        internal Monster(MonsterDto monsterDto, string name) : base(monsterDto.Id, name, EntityType.MONSTER) => _monsterDto = monsterDto;

        public int Vnum => _monsterDto.Id;
    }
}