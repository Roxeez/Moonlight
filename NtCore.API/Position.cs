namespace NtCore
{
    public struct Position
    {
        public short X { get; }
        public short Y { get; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}