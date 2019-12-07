using System.Collections.Generic;

namespace NtCore.API.Extensions
{
    public static class DictionaryExtension
    {
        /// <summary>
        ///     Get value or return default
        /// </summary>
        /// <param name="dictionary">Dictionary used</param>
        /// <param name="key">Key used to find value</param>
        /// <typeparam name="TKey">Type of key</typeparam>
        /// <typeparam name="TValue">Type of value</typeparam>
        /// <returns>Value of default</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }
    }
}