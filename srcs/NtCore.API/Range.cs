namespace NtCore.API
{
    public struct Range
    {
        public int Minimum { get; }
        public int Maximum { get; }

        public Range(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}