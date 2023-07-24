﻿using ConcaveHull;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcaveHull
{
    public static class HullFunctions
    {
        public static List<Line> GetDividedLine(Line line, List<Node> nearbyPoints, List<Line> concave_hull,
            double concavity)
        {
            // returns two lines if a valid middlePoint is found
            // returns empty list if the line can't be divided
            List<Line> dividedLine = new List<Line>();
            List<Node> okMiddlePoints = new List<Node>();
            foreach (Node middlePoint in nearbyPoints)
            {
                double _cos = GetCos(line.start, line.end, middlePoint);
                if (_cos < concavity)
                {
                    Line newLineA = new Line(line.start, middlePoint);
                    Line newLineB = new Line(middlePoint, line.end);
                    if (!LineCollidesWithHull(newLineA, concave_hull) && !LineCollidesWithHull(newLineB, concave_hull))
                    {
                        middlePoint.cos = _cos;
                        okMiddlePoints.Add(middlePoint);
                    }
                }
            }

            if (okMiddlePoints.Count > 0)
            {
                // We want the middlepoint to be the one with the widest angle (smallest cosine)
                okMiddlePoints = okMiddlePoints.OrderBy(p => p.cos).ToList();
                dividedLine.Add(new Line(line.start, okMiddlePoints[0]));
                dividedLine.Add(new Line(okMiddlePoints[0], line.end));
            }

            return dividedLine;
        }

        public static bool LineCollidesWithHull(Line line, List<Line> concave_hull)
        {
            foreach (Line hullLine in concave_hull)
            {
                // We don't want to check a collision with this point that forms the hull AND the line
                if (line.start.id != hullLine.start.id && line.start.id != hullLine.end.id
                                                             && line.end.id != hullLine.start.id &&
                                                             line.end.id != hullLine.end.id)
                {
                    // Avoid line interesections with the rest of the hull
                    if (LineIntersectionFunctions.DoIntersect(line.start, line.end, hullLine.start,
                            hullLine.end))
                        return true;
                }
            }

            return false;
        }

        private static double GetCos(Node A, Node B, Node O)
        {
            /* Law of cosines */
            double aPow2 = Math.Pow(A.x - O.x, 2) + Math.Pow(A.y - O.y, 2);
            double bPow2 = Math.Pow(B.x - O.x, 2) + Math.Pow(B.y - O.y, 2);
            double cPow2 = Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2);
            double cos = (aPow2 + bPow2 - cPow2) / (2 * Math.Sqrt(aPow2 * bPow2));
            return Math.Round(cos, 4);
        }

        public static List<Node> GetNearbyPoints(Line line, List<Node> nodeList, int scaleFactor)
        {
            /* The bigger the scaleFactor the more points it will return
             * Inspired by this precious algorithm:
             * http://www.it.uu.se/edu/course/homepage/projektTDB/ht13/project10/Project-10-report.pdf
             * Be carefull: if it's too small it will return very little points (or non!), 
             * if it's too big it will add points that will not be used and will consume time
             * */
            List<Node> nearbyPoints = new List<Node>();
            double[] boundary;
            int tries = 0;
            double node_x_rel_pos;
            double node_y_rel_pos;

            while (tries < 2 && nearbyPoints.Count == 0)
            {
                boundary = getBoundary(line, scaleFactor);
                foreach (Node node in nodeList)
                {
                    //Not part of the line
                    if (!(line.Contains(node)))
                    {
                        node_x_rel_pos = Math.Floor(node.x / scaleFactor);
                        node_y_rel_pos = Math.Floor(node.y / scaleFactor);
                        //Inside the boundary
                        if (node_x_rel_pos >= boundary[0] && node_x_rel_pos <= boundary[2] &&
                            node_y_rel_pos >= boundary[1] && node_y_rel_pos <= boundary[3])
                        {
                            nearbyPoints.Add(node);
                        }
                    }
                }

                //if no points are found we increase the area
                scaleFactor = scaleFactor * 4 / 3;
                tries++;
            }

            return nearbyPoints;
        }

        private static double[] getBoundary(Line line, int scaleFactor)
        {
            /* Giving a scaleFactor it returns an area around the line 
             * where we will search for nearby points 
             * */
            double[] boundary = new double[4];
            Node aNode = line.start;
            Node bNode = line.end;
            double min_x_position = Math.Floor(Math.Min(aNode.x, bNode.x) / scaleFactor);
            double min_y_position = Math.Floor(Math.Min(aNode.y, bNode.y) / scaleFactor);
            double max_x_position = Math.Floor(Math.Max(aNode.x, bNode.x) / scaleFactor);
            double max_y_position = Math.Floor(Math.Max(aNode.y, bNode.y) / scaleFactor);

            boundary[0] = min_x_position;
            boundary[1] = min_y_position;
            boundary[2] = max_x_position;
            boundary[3] = max_y_position;

            return boundary;
        }
    }
}