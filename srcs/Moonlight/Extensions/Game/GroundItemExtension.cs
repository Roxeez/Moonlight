using Moonlight.Core.Enums;
using Moonlight.Game.Entities;

namespace Moonlight.Extensions.Game
{
    public static class GroundItemExtension
    {
        public static bool IsGold(this GroundItem groundItem) => groundItem.CannotBePickedUp() && groundItem.Item.Data[0] == 70;
        public static bool IsLever(this GroundItem groundItem) => groundItem.CannotBePickedUp() && groundItem.Item.Data[0] == 1000 || groundItem.Item.Data[0] == 1001;
        private static bool CannotBePickedUp(this GroundItem groundItem) => groundItem.Item.BagType == BagType.MAIN && groundItem.Item.Type == 3 && groundItem.Item.SubType == 0;
    }
}