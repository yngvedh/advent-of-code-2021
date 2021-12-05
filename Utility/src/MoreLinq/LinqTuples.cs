using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC.MoreLinq
{
    public static class LinqTuples
    {
        public static IEnumerable<(T,T)> Pairs<T>(this IEnumerable<T> ts)
            where T : IComparable<T>
        {
            while(ts.Any())
            {
                var t1 = ts.First();
                var ts1 = ts.Skip(1);

                foreach(var t2 in ts1)
                    yield return (t1, t2);

                ts = ts1;
            }
        }

        public static IEnumerable<(T,T,T)> Triplets<T>(this IEnumerable<T> ts)
            where T : IComparable<T>
        {
            while(ts.Any())
            {
                var t1 = ts.First();
                var ts1 = ts.Skip(1);

                foreach(var (t2, t3) in ts1.Pairs())
                    yield return (t1, t2, t3);

                ts = ts1;
            }
        }

        public static T? FirstOrNullable<T>(this IEnumerable<T> ts) where T : struct
        {
            if (ts.Any()) return ts.First();
            else return (T?)null;
        }

        public static T? FirstOrNullable<T>(this IEnumerable<T> ts, Func<T,bool> pred) where T : struct =>
            ts.Any(pred) ? ts.First(pred) : (T?)null;

        public static IDictionary<T,S> ToDictionary<T,S>(this IEnumerable<(T,S)> pairs) where T: notnull
        {
            var dict = new Dictionary<T,S>();

            foreach (var pair in pairs)
            {
                dict.Add(pair.Item1, pair.Item2);
            }

            return dict;
        }
    }
}
