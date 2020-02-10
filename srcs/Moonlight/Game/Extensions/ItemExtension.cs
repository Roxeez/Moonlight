using Moonlight.Core.Enums;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Extensions
{
    public static class ItemExtension
    {
        public static bool IsPotion(this Item item) => item.BagType == BagType.MAIN && item.Type == 5 && item.SubType == 0;
        public static bool IsFood(this Item item) => item.BagType == BagType.ETC && item.Type == 1 && item.SubType == 0;
        public static bool IsSnack(this Item item) => item.BagType == BagType.ETC && item.Type == 2 && item.SubType == 0;
    }
}