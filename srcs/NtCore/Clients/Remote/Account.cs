using Newtonsoft.Json;

namespace NtCore.Clients.Remote
{
    public class Account
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        
        [JsonProperty(PropertyName = "platformGameAccountId")]
        public string PlatformAccountId { get; set; }
    }
}