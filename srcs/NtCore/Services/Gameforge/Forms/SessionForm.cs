using System.Runtime.Serialization;

namespace NtCore.Services.Gameforge.Forms
{
    [DataContract]
    public class SessionForm
    {
        [DataMember(Name =  "platformGameAccountId")]
        public string PlatformAccountId { get; set; }
    }
}