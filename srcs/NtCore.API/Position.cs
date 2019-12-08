namespace NtCore.API
{
    /// <summary>
    ///     Represent a 2D position
    /// </summary>
    public struct Position
    {
        /// <summary>
        ///     X axis
        /// </summary>
        public short X { get; }

        /// <summary>
        ///     Y axis
        /// </summary>
        public short Y { get; }

        /// <summary>
        ///     Create a new position
        /// </summary>
        /// <param name="x">Position on X axis</param>
        /// <param name="y">Position on Y asix</param>
        public Position(short x, short y)
        {
            X = x;
            Y = y;
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

        public override string ToString() => $"{X}/{Y}";
    }
}