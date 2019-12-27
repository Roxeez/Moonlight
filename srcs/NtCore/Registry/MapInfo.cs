using System.Runtime.Serialization;

namespace NtCore.Registry
{
    [DataContract]
    public class MapInfo
    {
        [DataMember]
        public string NameKey { get; private set; }
    }
}