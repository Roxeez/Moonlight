namespace NtCore.Extensions
{
    public static class CastExtensions
    {
        public static TDestination AsModifiable<TDestination>(this object source) where TDestination : class
        {
            return source as TDestination;
        }
    }
}