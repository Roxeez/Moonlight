using System.Collections.Generic;

namespace Moonlight.Core.Extensions
{
    internal static class DictionaryExtension
    {
        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V value = default) => dictionary.TryGetValue(key, out V result) ? result : value;
    }
}