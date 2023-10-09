namespace ProductsDemo.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static int IndexOfKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var index = 0;
            foreach (var pair in dictionary)
            {
                if (pair.Key!.Equals(key))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}