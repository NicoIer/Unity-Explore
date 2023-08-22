using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace ColliderTool.Tests
{
    [TestFixture]
    public class BoxMergerTests
    {
        [Test]
        public void TestMerge()
        {
            HashSet<Vector2Int> points = new HashSet<Vector2Int>
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
                
                new Vector2Int(2,1),
                
                new Vector2Int(2,2),
                
                new Vector2Int(3,3),
            };
            var res = BoxMerger.Merge(points);
            Assert.AreEqual(4, res.Count);
        }
    }
}