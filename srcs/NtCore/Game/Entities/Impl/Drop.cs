using System;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities.Impl
{
    public class Drop : IDrop
    {
        public Drop()
        {
            EntityType = EntityType.DROP;
            DropTime = DateTime.Now;
        }

        public int Id { get; set; }
        public int Vnum { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public IPlayer Owner { get; set; }
        public DateTime DropTime { get; set; }
        public bool IsGold => Vnum == 1046;
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public EntityType EntityType { get; }
    }
}