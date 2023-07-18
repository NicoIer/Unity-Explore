using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class GameManager: MonoBehaviour
    {
        public List<GameObject> poolObjects;
        public void Awake()
        {
            foreach (var poolObject in poolObjects)
            {
                ObjectPoolManager.Register(poolObject,poolObject.name);
            }
            ModelManager.Register<PlayerModel>();
        }
    }
}