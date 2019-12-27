using Newtonsoft.Json;

namespace NtCore.Services.Gameforge
{
    public class GameforgeAccount
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        
        [JsonProperty(PropertyName = "platformGameAccountId")]
        public string PlatformGameAccountId { get; set; }
    }
}