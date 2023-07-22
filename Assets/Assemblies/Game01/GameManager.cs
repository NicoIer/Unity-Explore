using System.Collections.Generic;
using Nico;
using UnityEngine;


namespace OneButtonGame
{
    public class GameManager : SceneSingleton<GameManager>
    {
        public List<GameObject> poolGameObjects;
        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
            foreach (var poolGameObject in poolGameObjects)
            {
                PoolGameObjectManager.Instance.Register(poolGameObject);
            }
        }
    }
}