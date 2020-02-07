using System;

namespace Moonlight.Core
{
    public struct Position : IEquatable<Position>
    {
        public short X { get; }
        public short Y { get; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Position other) => other.X == X && other.Y == Y;
    }
}