using System.Collections.Generic;
using NtCore.Extensions;
using NtCore.Resources;

namespace NtCore.I18N
{
    public class LanguageService : ILanguageService
    {
        private IDictionary<LanguageKey, IDictionary<string, string>> _translations = new Dictionary<LanguageKey, IDictionary<string, string>>();

        private readonly ResourceManager _resourceManager;

        public LanguageService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        
        public string GetTranslation(LanguageKey languageKey, string key) => _translations.GetValueOrDefault(languageKey)?.GetValueOrDefault(key) ?? key;

        public void Load(string languageKey)
        {
            _translations = new Dictionary<LanguageKey, IDictionary<string, string>>()
            {
                [LanguageKey.SKILL] = _resourceManager.Load<Dictionary<string, string>>($"lang._code_{languageKey}_Skill.json"),
                [LanguageKey.ITEM] = _resourceManager.Load<Dictionary<string, string>>($"lang._code_{languageKey}_Item.json"),
                [LanguageKey.MONSTER] = _resourceManager.Load<Dictionary<string, string>>($"lang._code_{languageKey}_monster.json"),
                [LanguageKey.MAP] = _resourceManager.Load<Dictionary<string, string>>($"lang._code_{languageKey}_MapIDData.json")
            };
        }
    }
}