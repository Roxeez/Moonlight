namespace NtCore.Clients.Remote
{
    public class Language
    {
        public static readonly Language FR = new Language("fr", "fr_FR");

        public string Name { get; }
        public string Locale { get; }
        
        private Language(string name, string locale)
        {
            Name = name;
            Locale = locale;
        }
    }
}