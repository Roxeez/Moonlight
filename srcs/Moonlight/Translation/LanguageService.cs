using System.Globalization;
using Moonlight.Core.Enums;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;

namespace Moonlight.Translation
{
    internal class LanguageService : ILanguageService
    {
        private readonly IStringRepository<TranslationDto> _repository;

        public LanguageService(IStringRepository<TranslationDto> repository) => _repository = repository;

        public Language Language { get; set; } = Language.EN;

        public string GetTranslation(RootKey rootKey, string key)
        {
            string fullKey = $"{Language}:{rootKey}:{key}".ToLower(CultureInfo.InvariantCulture);
            TranslationDto dto = _repository.Select(fullKey);
            if (dto == null)
            {
                return fullKey;
            }

            return dto.Value;
        }
    }
}