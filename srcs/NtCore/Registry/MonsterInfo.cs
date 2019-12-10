using Newtonsoft.Json;

namespace NtCore.Registry
{
    public class MonsterInfo
    {
        [JsonProperty]
        public string NameKey { get; private set; }
        
        [JsonProperty]
        public byte Level { get; private set; }
    }
}