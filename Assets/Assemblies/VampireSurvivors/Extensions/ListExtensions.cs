using System.Collections.Generic;

namespace VampireSurvivors.Extensions
{
    public static class ListExtensions
    {
        public static TElement Random<TElement>(this List<TElement> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}