using Newtonsoft.Json;

namespace NtCore.Registry
{
    public class ItemInfo
    {
        [JsonProperty]
        public string NameKey { get; private set; }
        
        [JsonProperty]
        public int InventoryTab { get; private set; }
        
        [JsonProperty]
        public int EquipmentSlot { get; private set; }
    }
}