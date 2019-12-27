using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NtCore.Services.Gameforge
{
    [DataContract]
    public class GameforgeAccount
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "platformGameAccountId")]
        public string PlatformGameAccountId { get; set; }
    }
}