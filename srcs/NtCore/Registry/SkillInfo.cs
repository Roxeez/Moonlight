using System.Runtime.Serialization;
using NtCore.Enums;

namespace NtCore.Registry
{
    [DataContract]
    public class SkillInfo
    {
        [DataMember]
        public string NameKey { get; private set; }

        [DataMember]
        public int Cooldown { get; private set; }

        [DataMember]
        public SkillType SkillType { get; private set; }

        [DataMember]
        public int MpCost { get; private set; }

        [DataMember]
        public int CastId { get; private set; }

        [DataMember]
        public int Range { get; private set; }

        [DataMember]
        public TargetingType TargetingType { get; private set; }
    }
}