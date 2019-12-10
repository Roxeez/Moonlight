namespace NtCore.I18N
{
    public enum LanguageKey
    {
        MONSTER,
        ITEM,
        SKILL
    }
    
    public interface ILanguageService
    {
        string GetTranslation(LanguageKey languageKey, string key);
    }
}