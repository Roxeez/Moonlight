using System;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Items;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public sealed class Drop
    {
        public Drop()
        {
            EntityType = EntityType.DROP;
            DropTime = DateTime.Now;
        }

        public int Id { get; internal set; }
        public IItem Item { get; internal set; }
        public int Amount { get; internal set; }
        public Player Owner { get; internal set; }
        public DateTime DropTime { get; internal set; }
        public bool IsGold => Item.Vnum == 1046;
        public IMap Map { get; internal set; }
        public Position Position { get; internal set; }
        public EntityType EntityType { get; }
    }
}