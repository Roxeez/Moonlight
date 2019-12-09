using JetBrains.Annotations;

namespace NtCore.Extensions
{
    public static class CastExtensions
    {
        /// <summary>
        ///     Readable cast
        /// </summary>
        /// <param name="source">Object to cast</param>
        /// <typeparam name="TDestination">Cast type</typeparam>
        /// <returns>Casted object or null</returns>
        public static TDestination As<TDestination>([NotNull] this object source) where TDestination : class => source as TDestination;
    }
}