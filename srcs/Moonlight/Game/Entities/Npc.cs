using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent any kind of Npc
    ///     It can be game npc but also player pets
    /// </summary>
    public class Npc : LivingEntity
    {
        private readonly MonsterDto _monsterDto;

        internal Npc(long id, MonsterDto monsterDto, string name) : base(id, name, EntityType.NPC) => _monsterDto = monsterDto;

        public int Vnum => _monsterDto.Id;
    }
}