namespace NtCore.Services.Gameforge
{
    public class Language
    {
        public static readonly Language FR = new Language("fr", "fr_FR");

        private Language(string name, string locale)
        {
            Name = name;
            Locale = locale;
        }

        public string Name { get; }
        public string Locale { get; }
    }
}