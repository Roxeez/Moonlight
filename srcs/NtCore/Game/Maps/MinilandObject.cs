using System;
using NtCore.Core;

namespace NtCore.Game.Maps
{
    public class MinilandObject : IEquatable<MinilandObject>
    {
        public MinilandObject(int vnum, int id, Position position)
        {
            Vnum = vnum;
            Id = id;
            Position = position;
        }

        /// <summary>
        /// Vnum of object
        /// </summary>
        public int Vnum { get; }
        
        /// <summary>
        /// Id of object
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Position in miniland
        /// </summary>
        public Position Position { get; }

        public bool Equals(MinilandObject other) => other != null && other.Id == Id;
    }
}