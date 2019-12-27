using System;

namespace NtCore
{
    /// <summary>
    ///     Represent a position on Map
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        private static readonly double Sqrt = Math.Sqrt(2);

        /// <summary>
        ///     Position on X axis
        /// </summary>
        public short X { get; set; }

        /// <summary>
        ///     Position on Y axis
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        ///     Create a new position
        /// </summary>
        /// <param name="x">Position on X axis</param>
        /// <param name="y">Position on Y axis</param>
        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Get distance between this position and destination
        /// </summary>
        /// <param name="destination">Destination position</param>
        /// <returns>Distance between both position</returns>
        public int GetDistance(Position destination)
        {
            int x = Math.Abs(X - destination.X);
            int y = Math.Abs(Y - destination.Y);

            int min = Math.Min(x, y);
            int max = Math.Max(x, y);

            return (int)(min * Sqrt + max - min);
        }

        public int GetDistanceX(Position destination) => Math.Abs(X - destination.X);

        public int GetDistanceY(Position destination) => Math.Abs(Y - destination.Y);

        /// <summary>
        ///     Check if the position is in range
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <param name="range">Range</param>
        /// <returns></returns>
        public bool IsInRange(Position position, int range) => GetDistance(position) <= range;

        /// <summary>
        ///     Check if the position is in area range
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <param name="range">Range of the area</param>
        /// <returns></returns>
        public bool IsInArea(Position position, int range)
        {
            int dx = Math.Abs(X - position.X);
            int dy = Math.Abs(Y - position.Y);
            return dx <= range && dy <= range && dx + dy <= range + range / 2;
        }

        public bool Equals(Position other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Position other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() * 397 ^ Y.GetHashCode();
            }
        }

        public override string ToString() => $"{X}:{Y}";
    }
}