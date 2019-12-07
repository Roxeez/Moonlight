using System.Collections.Generic;
using NtCore.API.Extensions;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Maps
{
    public class Miniland : Map, IMiniland
    {
        private readonly Dictionary<int, IMinilandObject> _minilandObjects;

        public Miniland() : base(20001)
        {
            _minilandObjects = new Dictionary<int, IMinilandObject>();
        }

        public string Owner { get; set; }
        public int Visitor { get; set; }
        public int TotalVisitor { get; set; }
        public string Message { get; set; }
        public IEnumerable<IMinilandObject> MinilandObjects => _minilandObjects.Values;

        public IMinilandObject GetMinilandObject(int id)
        {
            return _minilandObjects.GetValueOrDefault(id);
        }

        public void AddMinilandObject(IMinilandObject obj)
        {
            _minilandObjects[obj.Id] = obj;
        }
    }
}