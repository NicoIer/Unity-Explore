﻿#if UNITY_EDITORusing UnityEngine;using System.Collections.Generic;using Sirenix.OdinInspector;using UnityEngine.Serialization;namespace ConcaveHull{    public class Init : MonoBehaviour    {        public List<Node> points = new List<Node>(); //Used only for the demo        public Hull hull;        public string seed;        public int scaleFactor;        public int num;        public double concavity;        [Button]        public void GenerateHull()        {            points = RandomPoints(num);            hull = new Hull(points, concavity, scaleFactor);        }        public List<Node> RandomPoints(int num)        {            List<Node> ans = new List<Node>();            // This method is only used for the demo!            System.Random pseudorandom = new System.Random(seed.GetHashCode());            for (int x = 0; x < num; x++)            {                ans.Add(new Node(pseudorandom.Next(0, 100), pseudorandom.Next(0, 100), x));            }            return ans;            // //Delete nodes that share same position            // for (int pivot_position = 0; pivot_position < points.Count; pivot_position++) {            //     for (int position = 0; position < points.Count; position++) {            //         if (points[pivot_position].x == points[position].x && points[pivot_position].y == points[position].y            //             && pivot_position != position) {            //             points.RemoveAt(position);            //             position--;            //         }            //     }             // }            //        }        // Unity demo visualization        void OnDrawGizmos()        {            if (hull == null) return;            // Convex hull            Gizmos.color = Color.yellow;            for (int i = 0; i < hull.convexLines.Count; i++)            {                Line line = hull.convexLines[i];                Vector2 left = line.start.ToVector2();                Vector2 right = line.end.ToVector2();                Gizmos.DrawLine(left, right);            }            // Concave hull            Gizmos.color = Color.blue;            for (int i = 0; i < hull.concaveLines.Count; i++)            {                Line line = hull.concaveLines[i];                Vector2 left = line.start.ToVector2();                Vector2 right = line.end.ToVector2();                Gizmos.DrawLine(left, right);            }            // Dots            Gizmos.color = Color.red;            for (int i = 0; i < points.Count; i++)            {                Gizmos.DrawSphere(new Vector3((float)points[i].x, (float)points[i].y, 0), 0.5f);            }        }    }}#endif