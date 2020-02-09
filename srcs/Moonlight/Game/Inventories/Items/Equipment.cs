using Moonlight.Core.Enums;

namespace Moonlight.Game.Inventories.Items
{
    public class Equipment : InventoryItem
    {
        internal Equipment(Item item, int slot) : base(item, BagType.EQUIPMENT, slot) => Amount = 1;

        public RarityType Rarity { get; internal set; }
        public int Upgrade { get; internal set; }
    }
}