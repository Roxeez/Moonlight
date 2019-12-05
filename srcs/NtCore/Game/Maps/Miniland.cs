using System.Collections.Generic;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Maps
{
    public class Miniland : Map, IMiniland
    {
        private readonly List<IMinilandObject> _minilandObjects;
        
        public string Owner { get; set; }
        public IEnumerable<IMinilandObject> MinilandObjects => _minilandObjects;
        
        public Miniland() : base(20001)
        {
            _minilandObjects = new List<IMinilandObject>();
        }

        public void AddObject(IMinilandObject obj)
        {
            _minilandObjects.Add(obj);
        }
    }
}