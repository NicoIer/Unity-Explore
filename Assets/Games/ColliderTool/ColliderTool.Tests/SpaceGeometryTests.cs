using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

namespace ColliderTool.Tests
{
    [TestFixture]
    public class SpaceGeometryTests
    {
        [Test]
        public void LinePlaneIntersectionTest()
        {
            // x=0平面
            Surface surface = Surface.YZPlane(0);
            Vector3 point = new Vector3(1, 0, 0);
            Vector3 direction = new Vector3(-1, 0, 0);
            Vector3 intersection = SpaceGeometry.LineSurfaceIntersection(point, direction, surface);
            Assert.AreEqual(Vector3.zero, intersection);
        }

        [Test]
        public void PointSurfaceDistanceTest()
        {
            Surface surface = Surface.YZPlane(0);
            Vector3 point = new Vector3(1, 0, 0);
            float distance = SpaceGeometry.PointSurfaceDistance(point, surface);
            Assert.AreEqual(1, distance);
        }
    }
}