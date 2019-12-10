using System.Collections.Generic;
using JetBrains.Annotations;
using NtCore.Extensions;

namespace NtCore.I18N
{
    public class LanguageService : ILanguageService
    {
        private readonly IDictionary<LanguageKey, IDictionary<string, string>> _translations;

        public LanguageService(IDictionary<LanguageKey, IDictionary<string, string>> translations) => _translations = translations;

        [NotNull]
        public string GetTranslation(LanguageKey languageKey, string key)
        {
            return _translations.GetValueOrDefault(languageKey)?.GetValueOrDefault(key) ?? key;
        }
    }
}