using Moonlight.Core.Enums;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent a dropped item on ground
    /// </summary>
    public class Drop : Entity
    {
        internal Drop(long id, Item item, int amount) : base(id, item.Name, EntityType.DROP)
        {
            Item = item;
            Amount = amount;
        }

        public Item Item { get; }
        public int Amount { get; }
        public Player Owner { get; internal set; }
    }
}