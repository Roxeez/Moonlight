namespace NtCore.Core
{
    public class Range
    {
        public Range(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public int Minimum { get; }
        public int Maximum { get; }
    }
}