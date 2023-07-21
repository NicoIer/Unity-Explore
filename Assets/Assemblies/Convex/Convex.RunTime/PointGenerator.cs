using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Convex.RunTime
{
    public class PointGenerator : MonoBehaviour
    {
        public List<Vector2> points = new List<Vector2>();
        public List<Vector2> endPoints = new List<Vector2>();

        [Button]
        public void Random(int num = 30)
        {
            for (int i = 0; i < num; i++)
            {
                Vector2 pos = new Vector2(UnityEngine.Random.Range(-100f, 100f),
                    UnityEngine.Random.Range(-100f, 100f));
                points.Add(pos);
            }
        }

        [Button]
        public void Clear()
        {
            points.Clear();
            endPoints.Clear();
        }

        [Button]
        public void Concave()
        {
            //生成凹包
            endPoints.Clear();
            List<Vector2> points2D = new List<Vector2>();
            for (int i = 0; i < points.Count; i++)
            {
                points2D.Add(points[i]);
            }

            // endPoints = ConcaveHull.MakeHull(points2D);
        }

        [Button]
        public void Convex()
        {
            endPoints.Clear();
            //生成凸包
            List<Vector2> points2D = new List<Vector2>();
            for (int i = 0; i < points.Count; i++)
            {
                points2D.Add(points[i]);
            }

            endPoints = ConvexHull.MakeHull(points2D);
        }

        private void OnDrawGizmos()
        {
            if(points.Count==0)return;
            Gizmos.color = Color.green;
            foreach (var point in points)
            {
                Gizmos.DrawWireSphere(point, 1f);
            }
            
            if (endPoints.Count == 0) return;
            //绘制凸包 用线连接起来
            Gizmos.color = Color.red;
            for (int i = 0; i < endPoints.Count - 1; i++)
            {
                Vector2 point1 = endPoints[i];
                Vector2 point2 = endPoints[i + 1];
                Gizmos.DrawLine(point1, point2);
            }

            Gizmos.DrawLine(endPoints[0], endPoints[^1]);
        }
    }
}