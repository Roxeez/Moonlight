using NtCore.Enums;
using NtCore.Game.Battle;
using NtCore.Game.Battle.Impl;
using NtCore.I18N;
using NtCore.Registry;

namespace NtCore.Factory
{
    public class SkillFactory : ISkillFactory
    {
        private readonly IRegistry _registry;
        private readonly ILanguageService _languageService;

        public SkillFactory(IRegistry registry, ILanguageService languageService)
        {
            _registry = registry;
            _languageService = languageService;
        }
        
        public ISkill CreateSkill(int vnum)
        {
            SkillInfo skillInfo = _registry.GetSkillInfo(vnum);
            var skill = new Skill
            {
                Vnum = vnum,
                MpCost = skillInfo?.MpCost ?? 0,
                Name = _languageService.GetTranslation(LanguageKey.SKILL, skillInfo?.NameKey ?? $"{vnum}"),
                Cooldown = skillInfo?.Cooldown ?? 0,
                SkillType = (SkillType)(skillInfo?.SkillType ?? 0)
            };

            return skill;
        }
    }
}