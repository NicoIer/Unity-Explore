using System;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSurvivors
{
    public class Enemy: MonoBehaviour
    {
        public static List<Enemy> enemies;
        private void Awake()
        {
            if (enemies == null)
            {
                enemies=new List<Enemy>();
            }
            enemies.Add(this);
        }
    }
}