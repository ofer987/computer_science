using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnapSack.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> list, string middle = "")
        {
            var sb = new StringBuilder();

            sb.Append(list.First().ToString());

            foreach (var item in list.Skip(1))
            {
                sb.Append(middle);
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }
    }
}
