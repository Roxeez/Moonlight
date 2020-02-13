using Moonlight.Core.Enums.Translation;

namespace Moonlight.Translation
{
    public interface ILanguageService
    {
        Language Language { get; set; }
        string GetTranslation(RootKey rootKey, string key);
    }
}