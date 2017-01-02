using System.Collections.Generic;

namespace KnapSack.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> EarnupReverse<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }
    }
}
