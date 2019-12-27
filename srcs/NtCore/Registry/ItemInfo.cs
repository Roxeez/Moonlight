using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NtCore.Registry
{
    [DataContract]
    public class ItemInfo
    {
        [DataMember]
        public string NameKey { get; private set; }
        
        [DataMember]
        public int InventoryTab { get; private set; }
        
        [DataMember]
        public int EquipmentSlot { get; private set; }
    }
}