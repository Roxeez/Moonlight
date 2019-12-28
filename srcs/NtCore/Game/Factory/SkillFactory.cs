using NtCore.Game.Battle;
using NtCore.Game.Battle.Impl;
using NtCore.I18N;
using NtCore.Registry;

namespace NtCore.Game.Factory
{
    public class SkillFactory : ISkillFactory
    {
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;

        public SkillFactory(IRegistry registry, ILanguageService languageService)
        {
            _registry = registry;
            _languageService = languageService;
        }

        public ISkill CreateSkill(int vnum)
        {
            SkillInfo skillInfo = _registry.GetSkillInfo(vnum);
            if (skillInfo == null)
            {
                return new Skill(new SkillInfo());
            }

            var skill = new Skill(skillInfo)
            {
                Vnum = vnum,
                Name = _languageService.GetTranslation(LanguageKey.SKILL, skillInfo.NameKey)
            };

            return skill;
        }
    }
}