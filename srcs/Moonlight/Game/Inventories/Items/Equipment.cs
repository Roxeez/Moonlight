using Moonlight.Core.Enums;

namespace Moonlight.Game.Inventories.Items
{
    public class Equipment : ItemStack
    {
        internal Equipment(Item item) : base(item) => Amount = 1;

        public RarityType Rarity { get; internal set; }
        public int Upgrade { get; internal set; }
    }
}