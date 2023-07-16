using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VampireSurvivors
{
    public class Position : MonoBehaviour
    {
        public void RandomXY(Vector3 center, float xRange, float yRange)
        {
            float x = Random.Range(-xRange, xRange);
            float y = Random.Range(-yRange, yRange);
            transform.position = new Vector3(x, y, 0) + center;
        }
    }
}