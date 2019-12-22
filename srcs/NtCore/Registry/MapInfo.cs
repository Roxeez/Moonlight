using Newtonsoft.Json;

namespace NtCore.Registry
{
    public class MapInfo
    {
        [JsonProperty]
        public string NameKey { get; private set; }
    }
}