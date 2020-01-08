using System;
using JetBrains.Annotations;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Items;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public sealed class Drop : Entity
    {
        public Drop()
        {
            EntityType = EntityType.DROP;
            DropTime = DateTime.Now;
        }

        /// <summary>
        /// Item of the drop
        /// </summary>
        [NotNull]
        public Item Item { get; internal set; }
        
        /// <summary>
        /// Amount of item
        /// </summary>
        public int Amount { get; internal set; }
        
        /// <summary>
        /// Owner of the drop
        /// </summary>
        [CanBeNull]
        public Player Owner { get; internal set; }
        
        /// <summary>
        /// Time when item is dropped
        /// </summary>
        public DateTime DropTime { get; }
        
        /// <summary>
        /// Define if it's gold or not
        /// </summary>
        public bool IsGold => Item.Vnum == 1046;
    }
}