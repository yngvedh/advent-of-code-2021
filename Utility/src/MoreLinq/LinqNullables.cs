using System.Collections.Generic;
using System.Linq;

namespace AoC.MoreLinq
{
    public static class LinqNullables
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<T?> ts) where T : struct =>
            ts.Where(_ => _.HasValue).Select(_ => _!.Value);
        
        public static IEnumerable<T> Values<T>(this IEnumerable<T?> ts) where T : class =>
            ts.Where(_ => _ != null).Select(_ => _!);

    }
}
