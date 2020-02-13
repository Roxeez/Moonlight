using System;
using Moonlight.Core.Enums.Game;
using Moonlight.Core.Enums.Translation;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Game.Battle;
using Moonlight.Translation;

namespace Moonlight.Game.Factory.Impl
{
    internal class SkillFactory : ISkillFactory
    {
        private readonly ILanguageService _languageService;
        private readonly IRepository<SkillDto> _skillRepository;

        public SkillFactory(ILanguageService languageService, IRepository<SkillDto> skillRepository)
        {
            _languageService = languageService;
            _skillRepository = skillRepository;
        }

        public Skill CreateSkill(int vnum)
        {
            SkillDto skillDto = _skillRepository.Select(vnum);
            if (skillDto == null)
            {
                throw new InvalidOperationException("Unknown skill");
            }

            string name = _languageService.GetTranslation(RootKey.SKILL, skillDto.NameKey);
            return new Skill(name, skillDto);
        }
    }
}