using Moonlight.Game.Inventories;

namespace Moonlight.Extensions.Game
{
    public static class ItemInstanceExtension
    {
        public static bool IsPotion(this ItemInstance item) => item.Item.IsPotion();
        public static bool IsFood(this ItemInstance item) => item.Item.IsFood();
        public static bool IsSnack(this ItemInstance item) => item.Item.IsSnack();
        public static bool IsReturnWing(this ItemInstance item) => item.Item.IsReturnWing();
        public static bool IsReturnAmulet(this ItemInstance item) => item.Item.IsReturnAmulet();
        public static bool IsMinilandBell(this ItemInstance item) => item.Item.IsMinilandBell();
        public static bool IsMinigame(this ItemInstance item) => item.Item.IsMinigame();
    }
}