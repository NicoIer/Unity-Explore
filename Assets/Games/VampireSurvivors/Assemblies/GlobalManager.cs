using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace VampireSurvivors
{
    public class GlobalManager: MonoBehaviour
    {
        public List<GameObject> prefabs;

        private void Awake()
        {
            foreach (var prefab in prefabs)
            {
                ObjectPoolManager.Instance.Register(prefab);
            }
            Application.targetFrameRate = 60;
        }
    }
}