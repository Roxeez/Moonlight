using System;
using System.Linq;
using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Inventories.Items
{
    public class Item : IEquatable<Item>
    {
        internal Item(int vnum, string name)
        {
            Vnum = vnum;
            Name = name;
        }
        public int Vnum { get; }
        public string Name { get; }

        public int Type { get; internal set; }
        public int SubType { get; internal set; }
        public BagType BagType { get; internal set; }
        public short[] Data { get; internal set; }

        public bool Equals(Item other) => other != null && other.Vnum == Vnum;
    }
}