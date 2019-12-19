using System.Collections.Generic;
using NtCore.Extensions;

namespace NtCore.Game.Maps.Impl
{
    public class Miniland : Map, IMiniland
    {
        private readonly Dictionary<int, IMinilandObject> _minilandObjects;

        public Miniland() : base(20001) => _minilandObjects = new Dictionary<int, IMinilandObject>();

        public string Owner { get; set; }
        public int Visitor { get; set; }
        public int TotalVisitor { get; set; }
        public string Message { get; set; }
        public IEnumerable<IMinilandObject> MinilandObjects => _minilandObjects.Values;

        public IMinilandObject GetMinilandObject(int id) => _minilandObjects.GetValueOrDefault(id);

        public void AddMinilandObject(IMinilandObject obj)
        {
            _minilandObjects[obj.Id] = obj;
        }
    }
}