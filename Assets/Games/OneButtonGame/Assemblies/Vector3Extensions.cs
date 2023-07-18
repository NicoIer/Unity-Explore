using UnityEngine;

namespace Nico
{
    public static class Vector3Extensions
    {
        public static Vector3 RandomOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x+Random.Range(-range,range),vector3.y+Random.Range(-range,range),vector3.z+Random.Range(-range,range));
        }
        
        public static Vector3 RandomXYOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x+Random.Range(-range,range),vector3.y+Random.Range(-range,range),vector3.z);
        }
        
        public static Vector3 RandomXZOffset(this Vector3 vector3, float range)
        {
            return new Vector3(vector3.x+Random.Range(-range,range),vector3.y,vector3.z+Random.Range(-range,range));
        }
    }
}