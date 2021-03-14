using System;
using System.Collections.Generic;
using System.Text;

namespace Stunts
{
    public static class ListExtensions
    {
        public static void Assign<T>(this IList<T> source, Func<int, T, T> callback)
        {
            for (var i = 0; i < source.Count; i++)
                source[i] = callback(i, source[i]);
        } 
        
        public static void ForEach<T>(this IReadOnlyList<T> source, Action<int, T> callback)
        {
            for (var i = 0; i < source.Count; i++)
                callback(i, source[i]);
        }
    }
}
