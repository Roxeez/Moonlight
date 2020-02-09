using System;
using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Core.Enums;
using PropertyChanged;

namespace Moonlight.Game.Inventories
{
    [AddINotifyPropertyChangedInterface]
    public class Inventory
    {
        public Bag Equipment { get; }
        public Bag Main { get; }
        public Bag Etc { get; }
        public Bag Miniland { get; }
        public Bag Specialist { get; }
        public Bag Costume { get; }

        public int Gold { get; set; }
        
        public Inventory()
        {
            Equipment = new Bag(new List<InventoryItem>());
            Main = new Bag(new List<InventoryItem>());
            Etc = new Bag(new List<InventoryItem>());
            Miniland = new Bag(new List<InventoryItem>());
            Specialist = new Bag(new List<InventoryItem>());
            Costume = new Bag(new List<InventoryItem>());
        }

        internal bool Contains(InventoryItem inventoryItem)
        {
            Bag bag = GetBag(inventoryItem.BagType);
            if (bag == null)
            {
                return false;
            }

            return bag.Contains(inventoryItem);
        }
        
        internal Bag GetBag(BagType bagType)
        {
            switch (bagType)
            {
                case BagType.MAIN:
                    return Main;
                case BagType.ETC:
                    return Etc;
                case BagType.COSTUME:
                    return Costume;
                case BagType.EQUIPMENT:
                    return Equipment;
                case BagType.SPECIALIST:
                    return Specialist;
                case BagType.MINILAND:
                    return Miniland;
                default:
                    throw new InvalidOperationException("Unknown bag type");
            }
        }
    }
}