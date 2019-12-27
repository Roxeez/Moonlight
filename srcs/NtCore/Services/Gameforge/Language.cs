namespace NtCore.Services.Gameforge
{
    public class Language
    {
        public static readonly Language FR = new Language("fr", "fr_FR");

        public string Name { get; }
        public string Locale { get; }
        
        public Language(string name, string locale)
        {
            Name = name;
            Locale = locale;
        }
    }
}