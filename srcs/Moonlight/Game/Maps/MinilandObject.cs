using System.Collections.Generic;
using Moonlight.Core;
using Moonlight.Game.Inventories.Items;

namespace Moonlight.Game.Maps
{
    public class MinilandObject
    {
        internal MinilandObject(Item item, int slot, Position position)
        {
            Item = item;
            Slot = slot;
            Position = position;
        }

        public Item Item { get; }
        public int Slot { get; }
        public Position Position { get; }
    }

    public class Minigame : MinilandObject
    {
        internal Minigame(Item item, int slot, Position position) : base(item, slot, position) => Scores = new List<Range>();

        public List<Range> Scores { get; }
    }
}