using System.Collections.Generic;

namespace TanksGB.GameLogic.Services
{
    internal static class ListExtensions
    {
        public static T GetNext<T>(this List<T> list, T currentElement)
        {
            int current = list.IndexOf(currentElement);
            int next = (current + 1) % list.Count;
            return list[next];
        }

        public static T GetPrevious<T>(this List<T> list, T currentElement)
        {
            int current = list.IndexOf(currentElement);
            int previous = (current - 1 + list.Count) % list.Count;
            return list[previous];
        }
    }
}