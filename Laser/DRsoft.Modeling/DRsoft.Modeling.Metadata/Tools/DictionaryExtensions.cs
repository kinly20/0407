using System;
using System.Collections.Generic;
using System.Linq;

namespace DRsoft.Modeling.Metadata.Tools
{
    /// <summary>
    /// Extension methods for Dictionary.
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// This method is used to try to get a value in a dictionary if it does exists.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="dictionary">The collection object</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value of the key (or default value if key not exists)</param>
        /// <returns>True if key does exists in the dictionary</returns>
        internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// Gets a value from the dictionary with given key. Returns default value if can not find.
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key">Key to find the value</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }

        /// <summary>
        /// Gets a value from the dictionary with given key. Returns default value if can not find.
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }

        /// <summary>
        /// Gets a value from the dictionary with given key. Returns default value if can not find.
        /// </summary>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }
        public static Dictionary<TKey, TSource> ConvertToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return source.ConvertToDictionary(keySelector, (IEqualityComparer<TKey>)null);
        }

        public static Dictionary<TKey, TSource> ConvertToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
          IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            try
            {
                return source.ToDictionary(keySelector, comparer);
            }
            catch (ArgumentException argumentException)
            {
                ThrowWhenDuplicateKeys(source, keySelector, comparer, argumentException);

                throw;
            }
        }


        public static Dictionary<TKey, TElement> ConvertToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector == null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            try
            {
                return source.ToDictionary<TSource, TKey, TElement>(keySelector, elementSelector);
            }
            catch (ArgumentException argumentException)
            {
                ThrowWhenDuplicateKeys(source, keySelector, null, argumentException);

                throw;
            }
        }

        public static Dictionary<TKey, TElement> ConvertToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector == null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            try
            {
                return source.ToDictionary<TSource, TKey, TElement>(keySelector, elementSelector, comparer);
            }
            catch (ArgumentException argumentException)
            {
                ThrowWhenDuplicateKeys(source, keySelector, comparer, argumentException);

                throw;
            }
        }

        /// <summary>
        /// 对字典进行扩展
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">key</param>
        /// <param name="v">实体或者值类型</param>
        /// <returns></returns>
        internal static bool TryRemove<TKey, TElement>(this IDictionary<TKey, TElement> dic, TKey key, out TElement v)
        {
            var isSuccess = dic.TryGetValue(key, out v);
            if (isSuccess)
            {
                if (dic.Remove(key))
                {
                    return true;
                }
            }
            return false;
        }

        private static void ThrowWhenDuplicateKeys<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer, ArgumentException argumentException)
        {
            var keys = source.Select(keySelector).GroupBy(x => x, comparer);
            var duplicate = keys.FirstOrDefault(key => key.Count() > 1);

            if (duplicate != null)
                throw new InvalidOperationException($"存在重复的Key：{duplicate.Key}", argumentException);
        }
    }
}