using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NtCore.Registry
{
    [DataContract]
    public class MapInfo
    {
        [DataMember]
        public string NameKey { get; private set; }
    }
}