using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 空间几何的算法类
    /// </summary>
    public static class SpaceGeometry
    {
        public static Vector3 RaySurfaceIntersection(Ray ray, Surface surface)
        {
            return LineSurfaceIntersection(ray.origin, ray.direction, surface);
        }
        //线和平面的交点
        public static Vector3 LineSurfaceIntersection(Vector3 lineStart, Vector3 lineDirection, Surface surface)
        {
            lineDirection = lineDirection.normalized;
            //平面的方程
            //Ax + By + Cz + D = 0
            //线的方程
            //x = x0 + vx * t
            //y = y0 + vy * t
            //z = z0 + vz * t
            //代入平面方程
            //A(x0 + vx * t) + B(y0 + vy * t) + C(z0 + vz * t) + D = 0
            //t = -(A * x0 + B * y0 + C * z0 + D) / (A * vx + B * vy + C * vz)

            float t = -(surface.normal.x * lineStart.x + surface.normal.y * lineStart.y +
                        surface.normal.z * lineStart.z + surface.constant) / Vector3.Dot(surface.normal, lineDirection);
            return lineStart + lineDirection * t;
        }

        //点到平面的距离 
        public static float PointSurfaceDistance(Vector3 point, Surface surface)
        {
            //点到平面的距离
            //d = |Ax + By + Cz + D| / sqrt(A^2 + B^2 + C^2)
            return Mathf.Abs(surface.normal.x * point.x + surface.normal.y * point.y +
                             surface.normal.z * point.z + surface.constant) / surface.normal.magnitude;
        }
    }
}