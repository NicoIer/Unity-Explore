using UnityEngine;

namespace Convex.RunTime
{
    
    public static class Vector2Extension
    {
        public static float Cross(this Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }
    }
}