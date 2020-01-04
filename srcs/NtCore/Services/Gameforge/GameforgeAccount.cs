using System;
using System.Runtime.Serialization;

namespace NtCore.Services.Gameforge
{
    [DataContract]
    public class GameforgeAccount
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        
        [DataMember(Name = "displayName")]
        public string Name { get; set; }
        
        [DataMember(Name = "created")]
        public DateTime Created { get; set; }
        
        [DataMember(Name = "lastLogin")]
        public DateTime LastLogin { get; set; }
    }
}