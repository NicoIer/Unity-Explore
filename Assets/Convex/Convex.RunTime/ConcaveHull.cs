using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Convex.RunTime
{
    /// <summary>
    /// 凹包算法
    /// </summary>
    public class ConcaveHullRollBall
    {
        public List<Vector2> points;
        public List<Vector2> borderPoints;
        public float defaultRadius;
        public float step;
        public int numIter;

        public ConcaveHullRollBall(List<Vector2> points, float defaultRadius, float step)
        {
            this.defaultRadius = defaultRadius;
            this.step = step;
            this.numIter = 10;
            this.points = points;
            borderPoints = new List<Vector2>();
            MakeHull();
        }

        //凹包算法 - 滚球
        private List<Vector2> MakeHull()
        {
            foreach (var point in points)
            {
                Vector2 ballCenter = point;
                float radius = defaultRadius;
                for (int i = 0; i < numIter; i++)
                {
                    Vector2 farthestPoint = FindFarthestPoint(ballCenter, radius); // + step * i);
                    // 检查球的滚动路径与凹包边界是否相交
                    if (IntersectsBoundary(ballCenter, radius, farthestPoint))
                    {
                        ballCenter = GetIntersectionPoint(ballCenter, radius, farthestPoint);
                    }
                    else
                    {
                        break;
                    }
                }

                borderPoints.Add(ballCenter);
            }

            throw new NotImplementedException();
        }

        // 检查球的滚动路径与凹包边界是否相交
        bool IntersectsBoundary(Vector2 center, float radius, Vector2 neighbor)
        {
            
            throw new NotImplementedException();
        }

        // 计算球的滚动路径与凹包边界的交点
        Vector2 GetIntersectionPoint(Vector2 center, float radius, Vector2 neighbor)
        {

            throw new NotImplementedException();
        }

        public Vector2 FindFarthestPoint(Vector2 center, float radius)
        {
            float maxDistance = 0;
            Vector2 farthestPoint = center * 10000;
            foreach (var point in points)
            {
                float distance = Vector2.Distance(center, point);
                if (distance > maxDistance && distance < radius)
                {
                    maxDistance = distance;
                    farthestPoint = point;
                }
            }

            return farthestPoint;
        }

        // public  Vector2 FindStartPoint()
        // {
        //     //找到最左边的点
        //     Vector2 startPoint = points[0];
        //     for (int i = 1; i < points.Count; i++)
        //     {
        //         if (points[i].x < startPoint.x)
        //         {
        //             startPoint = points[i];
        //         }
        //     }
        //
        //     return startPoint;
        // }
    }
}