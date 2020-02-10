using System;
using System.Collections.ObjectModel;
using Moonlight.Core.Enums;
using Moonlight.Game.Entities;

namespace Moonlight.Game.Inventories
{
    public class Inventory
    {
        private readonly Character _character;
        
        public Bag Equipment { get; }
        public Bag Main { get; }
        public Bag Etc { get; }
        public Bag Miniland { get; }
        public Bag Specialist { get; }
        public Bag Costume { get; }

        public Inventory(Character character)
        {
            _character = character;
            
            Equipment = new Bag();
            Main = new Bag();
            Etc = new Bag();
            Miniland = new Bag();
            Specialist = new Bag();
            Costume = new Bag();
        }

        /// <summary>
        /// Move item to another slot in inventory
        /// </summary>
        /// <param name="item">Item to move</param>
        /// <param name="destinationSlot"> Destination slot</param>
        public void Move(ItemInstance item, int destinationSlot)
        {
            Move(item, destinationSlot, item.Amount);
        }

        /// <summary>
        /// Move amount of item to another slot in inventory
        /// </summary>
        /// <param name="item">Item to move</param>
        /// <param name="destinationSlot">Destination slot</param>
        /// <param name="amount">Amount of item to move</param>
        public void Move(ItemInstance item, int destinationSlot, int amount)
        {
            int slot = GetItemSlot(item);

            if (slot == -1)
            {
                return;
            }
            
            if (destinationSlot == slot)
            {
                return;
            }

            if (amount <= 0 || amount > item.Amount)
            {
                return;
            }
            
            _character.Client.SendPacket($"mvi {item.Item.BagType} {slot} {amount} {destinationSlot}");
        }
        
        /// <summary>
        /// Use an item in your inventory
        /// Item won't be used if not in your inventory
        /// </summary>
        /// <param name="item">Item to use</param>
        public void Use(ItemInstance item)
        {
            int slot = GetItemSlot(item);

            if (slot == -1)
            {
                return;
            }
            
            _character.Client.SendPacket($"u_i {(int)_character.EntityType} {_character.Id} {(int)item.Item.BagType} {slot} 0 0 ");
        }

        /// <summary>
        /// Drop item from your inventory to ground
        /// </summary>
        /// <param name="itemInstance">Item to drop</param>
        public void Drop(ItemInstance itemInstance)
        {
            Drop(itemInstance, itemInstance.Amount);
        }
        
        /// <summary>
        /// Drop item from your inventory to ground
        /// </summary>
        /// <param name="item">Item to drop</param>
        /// <param name="amount">Amount of item to drop</param>
        public void Drop(ItemInstance item, int amount)
        {
            int slot = GetItemSlot(item);

            if (slot == -1)
            {
                return;
            }

            if (amount <= 0)
            {
                return;
            }

            if (amount > item.Amount)
            {
                return;
            }
            
            _character.Client.SendPacket($"put {(int)item.Item.BagType} {slot} {amount}");
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

        internal int GetItemSlot(ItemInstance item)
        {
            return GetBag(item.Item.BagType).GetSlot(item);
        }
    }
}