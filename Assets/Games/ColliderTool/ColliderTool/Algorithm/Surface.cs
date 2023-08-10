using System;
using UnityEngine;

namespace ColliderTool
{
    //使用Ax + By + Cz +D= 0 来表示平面
    public struct Surface
    {
        public Vector3 normal;
        public float constant;

        public Surface(Vector3 normal, float constant)
        {
            this.normal = normal;
            this.constant = constant;
        }
        public static Surface ToSurface(float xMul, float yMul, float zMul, float constant)
        {
            Vector3 normal = new Vector3(xMul, yMul, zMul);
            if (normal == Vector3.zero)
            {
                throw new ArgumentException("normal can not be zero");
            }

            return new Surface
            {
                normal = normal,
                constant = constant
            };
        }

        public static Surface XYPlane(float z)
        {
            return ToSurface(0, 0, 1, z);
        }

        public static Surface XZPlane(float y)
        {
            return ToSurface(0, 1, 0, y);
        }

        public static Surface YZPlane(float x)
        {
            return ToSurface(1, 0, 0, x);
        }
    }
}