using NtCore.Game.Battle;
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

        public Skill CreateSkill(int vnum)
        {
            SkillInfo skillInfo = _registry.GetSkillInfo(vnum);
            
            string name = _languageService.GetTranslation(LanguageKey.SKILL, skillInfo?.NameKey ?? $"{vnum}");
            
            return new Skill(vnum, name, skillInfo);
        }
    }
}