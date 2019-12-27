using System.Runtime.Serialization;

namespace NtCore.Registry
{
    [DataContract]
    public class MonsterInfo
    {
        [DataMember]
        public string NameKey { get; private set; }

        [DataMember]
        public byte Level { get; private set; }
    }
}