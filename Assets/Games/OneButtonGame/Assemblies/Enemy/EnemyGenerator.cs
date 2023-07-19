using System;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class EnemyGenerator : MonoBehaviour
    {
        public float generateInterval = 1f;
        public float generateTimer = 0;
        public int generateNum = 2;
        public float range = 30;

        private void Update()
        {
            generateTimer+=Time.deltaTime;
            if (generateTimer >= generateInterval)
            {
                generateTimer = 0;
                Generate();
            }
        }

        public void Generate()
        {
            for (int i = 0; i < generateNum; i++)
            {
                Enemy enemy = ObjectPoolManager.Instance.Get(nameof(Enemy)).GetComponent<Enemy>();
                enemy.transform.position = SpaceShip.Instance.transform.position.RandomXYOffset(range);
            }
        }
    }
}