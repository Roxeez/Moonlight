namespace NtCore
{
    public class Range
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