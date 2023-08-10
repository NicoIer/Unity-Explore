using System;
using UnityEngine;

namespace ColliderTool
{
    public struct Surface
    {
        private Vector3 _normal;
        private Vector3 _point;

        public float Constant => Vector3.Dot(_normal, _point); // Ax + By + Cz = D 求D
        public Vector3 Normal => _normal.normalized;

        /// <summary>
        /// AX + BY + CZ = D
        /// </summary>
        /// <param name="xMul"></param>
        /// <param name="yMul"></param>
        /// <param name="zMul"></param>
        /// <param name="constant"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Surface ToSurface(float xMul, float yMul, float zMul, float constant)
        {
            Vector3 normal = new Vector3(xMul, yMul, zMul);
            if (normal == Vector3.zero)
            {
                throw new ArgumentException("normal can not be zero");
            }

            Vector3 point = new Vector3(0, 0, constant);
            // xMul*X + yMul*Y + zMul*Z = constant
            return new Surface()
            {
                _normal = normal,
                _point = point
            };
        }

        public static Surface XYPlane(float z)
        {
            return new Surface()
            {
                _normal = Vector3.forward,
                _point = new Vector3(0, 0, z)
            };
        }

        public static Surface XZPlane(float y)
        {
            return new Surface()
            {
                _normal = Vector3.up,
                _point = new Vector3(0, y, 0)
            };
        }

        public static Surface YZPlane(float x)
        {
            return new Surface()
            {
                _normal = Vector3.right,
                _point = new Vector3(x, 0, 0)
            };
        }
    }
}