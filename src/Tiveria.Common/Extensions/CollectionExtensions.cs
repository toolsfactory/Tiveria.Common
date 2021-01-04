using System;
using System.Linq;
using System.Collections.Generic;

namespace Tiveria.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void Do<T>(this IEnumerable<T> @this, Func<T, object> lambda)
        {
            foreach (var item in @this)
                lambda(item);
        }

        public static T Random<T>(this IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            int count = items.Count();
            if (count == 0)
                return default(T);

            var rand = new Random();
            return items.ElementAt(rand.Next(count));
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

    }
}
