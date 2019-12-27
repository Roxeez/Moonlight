using JetBrains.Annotations;

namespace NtCore.I18N
{
    public interface ILanguageService
    {
        /// <summary>
        ///     Get the translation according to language key & key
        /// </summary>
        /// <param name="languageKey">Language key</param>
        /// <param name="key">Key</param>
        /// <returns>Translation or key</returns>
        [NotNull]
        string GetTranslation(LanguageKey languageKey, [NotNull] string key);

        void Load(string languageKey);
    }
}