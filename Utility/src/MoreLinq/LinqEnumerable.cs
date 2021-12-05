using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.MoreLinq;
public static class LinqEnumerable
{
    public static IEnumerable<IEnumerable<T>> Windows<T>(this IEnumerable<T> ts, int windowSize)
        => ts.Tails().Where(_ => _.Count() >= windowSize).Select(_ => _.Take(windowSize));

    public static IEnumerable<T> Trace<T>(this IEnumerable<T> ts, Func<T,string> describe)
    {
        foreach(var t in ts)
        {
            Console.WriteLine($"trace: " + t);
            yield return t;
        }
    }

    public static IEnumerable<IEnumerable<T>> Tails<T>(this IEnumerable<T> ts)
        => Enumerable.Range(0, ts.Count()).Select(_ => ts.Skip(_));

    public static IEnumerable<IEnumerable<T>> Heads<T>(this IEnumerable<T> ts)
        => Enumerable.Range(0, ts.Count()).Select(_ => ts.Take(_ + 1));

    public static Dictionary<S,T> ToDictionary<S,T>(IEnumerable<KeyValuePair<S,T>> ts) where S : notnull
        => ts.ToDictionary(_ => _.Key, _ => _.Value);

    public static Dictionary<K, T> SelectValues<K, S, T>(this Dictionary<K, S> d, Func<S, T> selector) where K : notnull
        => new Dictionary<K, T>(d.Select(kv => KeyValuePair.Create(kv.Key, selector(kv.Value))));

    public static Dictionary<K, T> SelectValues<K, S, T>(
        this Dictionary<K, S> d,
        Func<KeyValuePair<K, S>, T> selector) where K : notnull
        => new Dictionary<K, T>(d.Select(kv => KeyValuePair.Create(kv.Key, selector(kv))));
}
