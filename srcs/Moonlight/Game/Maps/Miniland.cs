using System.Collections.Generic;

namespace Moonlight.Game.Maps
{
    public class Miniland : Map
    {
        internal Miniland(string name, byte[] grid) : base(20001, name, grid) => Objects = new List<MinilandObject>();

        public string Owner { get; internal set; }
        public string Message { get; internal set; }
        public List<MinilandObject> Objects { get; }
    }
}