namespace MyExtensions
{
    using System;
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        // Basic foreach template like .ForEach but for all IEnumerable types
        public static IEnumerable<T> ForEach<T>(
            this IEnumerable<T> collection, 
            Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        // Basic template like .Where for all collections 
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> collection,
            Func<T, bool> action)
        {
            var result = new List<T>();
            foreach (var item in collection)
            {
                if (action(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
