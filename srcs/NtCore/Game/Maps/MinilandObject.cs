using System;
using NtCore.Core;

namespace NtCore.Game.Maps
{
    public class MinilandObject : IEquatable<MinilandObject>
    {
        public int Vnum { get; }
        public int Id { get;}
        public Position Position { get; }

        public MinilandObject(int vnum, int id, Position position)
        {
            Vnum = vnum;
            Id = id;
            Position = position;
        }

        public bool Equals(MinilandObject other) => other != null && other.Id == Id;
    }
}