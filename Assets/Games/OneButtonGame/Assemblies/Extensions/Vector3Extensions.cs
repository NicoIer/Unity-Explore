using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nico
{
    public static class Vector3Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x + Random.Range(-range, range), vector3.y + Random.Range(-range, range),
                vector3.z + Random.Range(-range, range));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomXYOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x + Random.Range(-range, range), vector3.y + Random.Range(-range, range),
                vector3.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomXZOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x + Random.Range(-range, range), vector3.y,
                vector3.z + Random.Range(-range, range));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 position(this GameObject gameObject)
        {
            return gameObject.transform.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 position(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.transform.position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 localPosition(this GameObject gameObject)
        {
            return gameObject.transform.localPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 localPosition(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.transform.localPosition;
        }
    }
}