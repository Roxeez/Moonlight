using Moonlight.Core.Enums;
using NFluent;

namespace Moonlight.Tests.Extensions
{
    public static class CheckExtension
    {
        public static void Is(this ICheck<BagType> check, BagType bagType)
        {
            check.IsEqualTo(bagType);
        }

        public static void Is<T>(this ICheck<T> check, T value)
        {
            check.IsEqualTo(value);
        }
    }
}