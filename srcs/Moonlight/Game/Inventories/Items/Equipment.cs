using Moonlight.Core.Enums;

namespace Moonlight.Game.Inventories.Items
{
    public class Equipment : ItemInstance
    {
        internal Equipment(Item item, int slot, RarityType rarity, int upgrade) : base(item, BagType.EQUIPMENT, slot, 1)
        {
            Rarity = rarity;
            Upgrade = upgrade;
        }

        public RarityType Rarity { get; }
        public int Upgrade { get; }
    }
}