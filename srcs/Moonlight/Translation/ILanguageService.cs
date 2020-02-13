using Moonlight.Core.Enums;

namespace Moonlight.Translation
{
    internal interface ILanguageService
    {
        Language Language { get; set; }
        string GetTranslation(RootKey rootKey, string key);
    }
}