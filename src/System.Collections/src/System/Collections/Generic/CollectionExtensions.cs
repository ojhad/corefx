// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        [return: MaybeNull]
        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TKey : object
        {
            return dictionary.GetValueOrDefault(key, default(TValue)!); // TODO-NULLABLE: Remove ! when nullable attributes are respected
        }

        [return: MaybeNull]
        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, [AllowNull] TValue defaultValue) where TKey : object
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            TValue value;            
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : object
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return true;
            }

            return false;
        }

        public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, [MaybeNullWhen(false)] out TValue value) where TKey : object
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary.TryGetValue(key, out value))
            {
                dictionary.Remove(key);
                return true;
            }

            value = default(TValue)!;
            return false;
        }
    }
}
