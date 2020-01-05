using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Entities;
using NtCore.I18N;
using NtCore.Registry;

namespace NtCore.Game.Factory
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IItemFactory _itemFactory;
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;

        public EntityFactory(ILanguageService languageService, IRegistry registry, IItemFactory itemFactory)
        {
            _languageService = languageService;
            _registry = registry;
            _itemFactory = itemFactory;
        }

        public Monster CreateMonster(int id, int vnum, Position position, byte direction, byte hpPercentage, byte mpPercentage)
        {
            MonsterInfo monsterInfo = _registry.GetMonsterInfo(vnum);
            var monster = new Monster
            {
                Id = id,
                Vnum = vnum,
                Level = monsterInfo?.Level ?? 1,
                Position = position,
                Direction = direction,
                HpPercentage = hpPercentage,
                MpPercentage = mpPercentage,
                Name = _languageService.GetTranslation(LanguageKey.MONSTER, monsterInfo?.NameKey ?? $"{vnum}")
            };

            return monster;
        }

        public Npc CreateNpc(int id, int vnum, Position position, byte direction, byte hpPercentage, byte mpPercentage)
        {
            MonsterInfo monsterInfo = _registry.GetMonsterInfo(vnum);
            var npc = new Npc
            {
                Id = id,
                Vnum = vnum,
                Level = monsterInfo?.Level ?? 1,
                Position = position,
                Direction = direction,
                HpPercentage = hpPercentage,
                MpPercentage = mpPercentage,
                Name = _languageService.GetTranslation(LanguageKey.MONSTER, monsterInfo?.NameKey ?? $"{vnum}")
            };

            return npc;
        }

        public Drop CreateDrop(int id, int vnum, int amount, Position position, Player owner)
        {
            var drop = new Drop
            {
                Id = id,
                Amount = amount,
                Position = position,
                Owner = owner,
                Item = _itemFactory.CreateItem(vnum)
            };

            return drop;
        }

        public Player CreatePlayer(int id, string name, byte level, ClassType classType, byte direction, Gender gender, Position position, byte hpPercentage, byte mpPercentage)
        {
            var player = new Player
            {
                Id = id,
                Name = name,
                Level = level,
                Class = classType,
                Direction = direction,
                Gender = gender,
                Position = position,
                HpPercentage = hpPercentage,
                MpPercentage = mpPercentage
            };

            return player;
        }
    }
}