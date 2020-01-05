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

        public int Vnum { get; }
        public int Id { get; }
        public Position Position { get; }

        public bool Equals(MinilandObject other) => other != null && other.Id == Id;
    }
}