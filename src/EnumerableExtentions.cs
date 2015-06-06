using System;
using System.Collections.Generic;

namespace CSRotoZoomer
{
    internal static class EnumerableExtentions
    {
        public static T FirstInXWhere<T>(IEnumerable<T> list, Func<T, bool> match)
        {
            foreach (var item in list)
            {
                if (match(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("Item not found.");
        }
    }
}