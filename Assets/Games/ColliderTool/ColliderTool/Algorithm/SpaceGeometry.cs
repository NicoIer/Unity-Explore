using System;
using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 空间几何的算法类
    /// </summary>
    public static class SpaceGeometry
    {
		//点到平面的距离
        public static Vector3 LineSurfaceIntersection(Vector3 lineStart, Vector3 lineDirection, Surface surface)
        {
            //Surface: Ax + By + Cz = D
            //Line: P = P0 + tV
            //t = (D - N.P0) / N.V
            //P0: lineStart
            //V: lineDirection
            //P: intersection
            float t = (surface.Constant - Vector3.Dot(surface.Normal, lineStart)) /
                      Vector3.Dot(surface.Normal, lineDirection);
            return lineStart + t * lineDirection;
        }

        //点到平面的距离 
        public static float SurfaceDistance(Vector3 point, Surface surface)
        {
            return Vector3.Dot(surface.Normal, point) - surface.Constant;
        }
    }
}