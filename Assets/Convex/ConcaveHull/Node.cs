using System;
using UnityEngine;

namespace ConcaveHull
{
    [Serializable]
    public class Node
    {
        public double x;
        public double y;
        // public double cos; // Used for middlepoint calculations

        public Node(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2((float)x, (float)y);
        }
    }
}