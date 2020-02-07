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
            string fullKey = $"{Language}:{rootKey}:{key}".ToLower();
            return _repository.Find(fullKey)?.Value ?? fullKey;
        }
    }
}