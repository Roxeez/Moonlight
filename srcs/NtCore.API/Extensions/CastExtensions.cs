namespace NtCore.API.Extensions
{
    public static class CastExtensions
    {
        public static TDestination As<TDestination>(this object source) where TDestination : class
        {
            return source as TDestination;
        }
    }
}