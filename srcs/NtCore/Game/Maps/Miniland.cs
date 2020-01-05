using System.Collections.Generic;
using NtCore.Extensions;

namespace NtCore.Game.Maps
{
    public class Miniland : Map
    {
        private readonly Dictionary<int, MinilandObject> _minilandObjects;

        public Miniland(byte[] data) : base(20001, data) => _minilandObjects = new Dictionary<int, MinilandObject>();

        public string Owner { get; internal set; }
        public int Visitor { get; internal set; }
        public int TotalVisitor { get; internal set; }
        public string Message { get; internal set; }
        public IEnumerable<MinilandObject> MinilandObjects => _minilandObjects.Values;

        public MinilandObject GetMinilandObject(int id) => _minilandObjects.GetValueOrDefault(id);

        public void AddMinilandObject(MinilandObject obj)
        {
            _minilandObjects[obj.Id] = obj;
        }
    }
}