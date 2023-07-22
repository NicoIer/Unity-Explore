using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Convex.RunTime
{

    public static class ConvexHull
    {
        public const float TOLERANCE = 0.0000001f;

        public static List<Vector2> MakeHull(List<Vector2> points)
        {
            // Sort points lexicographically
            points.Sort((a, b) =>
            {
                if (Math.Abs(a.x - b.x) > TOLERANCE)
                    return a.x.CompareTo(b.x);
                return a.y.CompareTo(b.y);
            });
            // Build lower hull
            List<Vector2> lower = new List<Vector2>();
            for (int i = 0; i < points.Count; i++)
            {
                while (lower.Count >= 2 && Cross(lower[lower.Count - 2], lower[lower.Count - 1], points[i]) <= 0)
                {
                    lower.RemoveAt(lower.Count - 1);
                }

                lower.Add(points[i]);
            }

            // Build upper hull
            List<Vector2> upper = new List<Vector2>();
            for (int i = points.Count - 1; i >= 0; i--)
            {
                while (upper.Count >= 2 && Cross(upper[upper.Count - 2], upper[upper.Count - 1], points[i]) <= 0)
                {
                    upper.RemoveAt(upper.Count - 1);
                }

                upper.Add(points[i]);
            }

            // Last point of upper list is omitted because it is repeated at the beginning of the lower list.
            upper.RemoveAt(upper.Count - 1);
            // Concatenation of the lower and upper hulls gives the convex hull.
            return lower.Concat(upper).ToList();
        }

        private static float Cross(Vector2 O, Vector2 A, Vector2 B)
        {
            return (A.x - O.x) * (B.y - O.y) - (A.y - O.y) * (B.x - O.x);
        }

        public static List<Vector2> ConvexFiy()
        {
            throw new NotImplementedException();
        }
    }
}