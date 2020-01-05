namespace NtCore.Services.Gameforge
{
    public class Language
    {
        public static readonly Language FR = new Language("fr", "fr_FR");
        public static readonly Language EN = new Language("en", "en_GB");
        public static readonly Language DE = new Language("de", "de_DE");
        public static readonly Language IT = new Language("it", "it_IT");
        public static readonly Language ES = new Language("es", "es_ES");
        public static readonly Language PL = new Language("pl", "pl_PL");

        private Language(string name, string locale)
        {
            Name = name;
            Locale = locale;
        }

        public string Name { get; }
        public string Locale { get; }
    }
}