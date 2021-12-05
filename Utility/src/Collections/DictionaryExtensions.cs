using System.Collections.Generic;
using System.Linq;

namespace AoC.Collections
{
    public static class DictionaryExtensions
    {
        public static S? GetOrNull<T,S>(this IDictionary<T,S> d, T key) where S : class =>
            d.ContainsKey(key) ? d[key] : null;

        public static S? GetOrNullable<T,S>(this IDictionary<T,S> d, T key) where S : struct =>
            d.ContainsKey(key) ? d[key] : null;

        public static D Merge<D, T,S>(this D da, IEnumerable<KeyValuePair<T,S>> db) where D : IDictionary<T,S>, new()
        {
            D dd = new D();
            
            foreach(var p in db) dd[p.Key] = p.Value;
            foreach(var p in da) dd[p.Key] = p.Value;

            return dd;
        }

        public static D Merge<D, T, S>(this D da, IEnumerable<(T,S)> db) where D : IDictionary<T,S>, new()
        {
            D dd = new D();
            
            foreach(var p in db) dd[p.Item1] = p.Item2;
            foreach(var p in da) dd[p.Key] = p.Value;

            return dd;
        }
    }
}