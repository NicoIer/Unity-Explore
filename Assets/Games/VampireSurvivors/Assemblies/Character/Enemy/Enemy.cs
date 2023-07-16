using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace VampireSurvivors
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 3f;
        public static List<Enemy> enemies { get; protected set; }

        private void Awake()
        {
            if (enemies == null)
            {
                enemies = new List<Enemy>();
            }

            enemies.Add(this);
        }

        public void Update()
        {
            transform.MoveTo(Player.Instance.transform, speed,Time.deltaTime);
        }
    }
}