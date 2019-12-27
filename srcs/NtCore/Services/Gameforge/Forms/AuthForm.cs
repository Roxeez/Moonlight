using System.Runtime.Serialization;

namespace NtCore.Services.Gameforge.Forms
{
    [DataContract]
    public class AuthForm
    {
        [DataMember(Name = "gfLang")]
        public string GfLang { get; set; }

        [DataMember(Name = "identity")]
        public string Identity { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "platformGameId")]
        public string PlatformGameId { get; set; }
    }
}