namespace NtCore.I18N
{
    public interface ILanguageService
    {
        string GetTranslation(LanguageKey languageKey, string key);
    }
}