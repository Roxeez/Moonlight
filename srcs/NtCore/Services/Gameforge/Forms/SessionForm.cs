using Newtonsoft.Json;

namespace NtCore.Services.Gameforge.Forms
{
    public class SessionForm
    {
        [JsonProperty(PropertyName = "platformGameAccountId")]
        public string PlatformAccountId { get; set; }
    }
}