using Newtonsoft.Json;
using NtCore.Enums;

namespace NtCore.Registry
{
    public class SkillInfo
    {
        [JsonProperty]
        public string NameKey { get; private set; }
        
        [JsonProperty]
        public int Cooldown { get; private set; }
        
        [JsonProperty]
        public SkillType SkillType { get; private set; }
        
        [JsonProperty]
        public int MpCost { get; private set; }
        
        [JsonProperty]
        public int CastId { get; private set; }
        
        [JsonProperty]
        public int Range { get; private set; }
        
        [JsonProperty]
        public TargetingType TargetingType { get; private set; }
    }
}