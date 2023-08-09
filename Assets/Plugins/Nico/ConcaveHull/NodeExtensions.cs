#if UNITY_64
using ConcaveHull;
using UnityEngine;

namespace ColliderEditor
{
    public static class NodeExtensions
    {
        public static UnityEngine.Vector2 ToVector2(this Node node)
        {
            return new UnityEngine.Vector2((float)node.x, (float)node.y);
        }
    }
}
#endif
