using Newtonsoft.Json;

namespace NtCore.Services.Gameforge.Forms
{
    public class AuthForm
    {
        [JsonProperty(PropertyName = "gfLang")]
        public string GfLang { get; set; }
        
        [JsonProperty(PropertyName = "identity")]
        public string Identity { get; set; }
        
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
        
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "platformGameId")]
        public string PlatformGameId { get; set; }
    }
}