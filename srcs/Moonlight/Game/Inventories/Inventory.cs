using System.Collections.Generic;
using Moonlight.Core.Enums;
using Moonlight.Core.Extensions;

namespace Moonlight.Game.Inventories
{
    public class Inventory
    {
        private readonly Dictionary<BagType, Bag> _bags;

        internal Inventory() => _bags = new Dictionary<BagType, Bag>();

        public int Gold { get; internal set; }

        public Bag GetBag(BagType bagType) => _bags.GetValueOrDefault(bagType);

        internal void AddBag(BagType bagType, Bag bag)
        {
            _bags[bagType] = bag;
        }
    }
}