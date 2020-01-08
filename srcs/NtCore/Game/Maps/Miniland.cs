using System.Collections.Generic;
using JetBrains.Annotations;
using NtCore.Extensions;

namespace NtCore.Game.Maps
{
    public class Miniland : Map
    {
        private readonly Dictionary<int, MinilandObject> _minilandObjects;

        public Miniland(byte[] data) : base(20001, data) => _minilandObjects = new Dictionary<int, MinilandObject>();

        /// <summary>
        /// Name of miniland owner
        /// </summary>
        public string Owner { get; internal set; }
        
        /// <summary>
        /// Amount of visitor (today)
        /// </summary>
        public int Visitor { get; internal set; }
        
        /// <summary>
        /// Amount of visitor (total)
        /// </summary>
        public int TotalVisitor { get; internal set; }
        
        /// <summary>
        /// Welcome message
        /// </summary>
        public string Message { get; internal set; }
        
        /// <summary>
        /// All miniland objects
        /// </summary>
        public IEnumerable<MinilandObject> MinilandObjects => _minilandObjects.Values;

        /// <summary>
        /// Get miniland object by id
        /// </summary>
        /// <param name="id">Id of the object</param>
        /// <returns>Object found or null if none</returns>
        [CanBeNull]
        public MinilandObject GetMinilandObject(int id) => _minilandObjects.GetValueOrDefault(id);

        internal void AddMinilandObject(MinilandObject obj)
        {
            _minilandObjects[obj.Id] = obj;
        }
    }
}