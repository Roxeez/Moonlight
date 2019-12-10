using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.I18N;
using NtCore.Registry;

namespace NtCore.Factory
{
    public class EntityFactory : IEntityFactory
    {
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;
        
        public EntityFactory(ILanguageService languageService, IRegistry registry)
        {
            _languageService = languageService;
            _registry = registry;
        }

        public Monster CreateMonster(int vnum)
        {
            MonsterInfo monsterInfo = _registry.GetMonsterInfo(vnum);
            var monster = new Monster
            {
                Vnum = vnum,
                Level = monsterInfo?.Level ?? 1,
                Name = _languageService.GetTranslation(LanguageKey.MONSTER, monsterInfo?.NameKey ?? $"{vnum}")
            };

            return monster;
        }

        public Npc CreateNpc(int vnum)
        {
            MonsterInfo monsterInfo = _registry.GetMonsterInfo(vnum);
            var npc = new Npc
            {
                Vnum = vnum,
                Level = monsterInfo?.Level ?? 1,
                Name = _languageService.GetTranslation(LanguageKey.MONSTER, monsterInfo?.NameKey ?? $"{vnum}")
            };

            return npc;
        }

        public Drop CreateDrop(int vnum)
        {
            ItemInfo itemInfo = _registry.GetItemInfo(vnum);
            var drop = new Drop
            {
                Vnum = vnum,
                Name = _languageService.GetTranslation(LanguageKey.ITEM, itemInfo?.NameKey ?? $"{vnum}")
            };

            return drop;
        }
    }
}