using Newtonsoft.Json;

namespace NtCore.Registry
{
    public class SkillInfo
    {
        [JsonProperty]
        public string NameKey { get; private set; }
        
        [JsonProperty]
        public int Cooldown { get; private set; }
        
        [JsonProperty]
        public byte SkillType { get; private set; }
        
        [JsonProperty]
        public int MpCost { get; private set; }
    }
}