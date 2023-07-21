using System.Collections.Generic;
using UnityEngine;


namespace OneButtonGame
{
    public class GameManager : MonoBehaviour
    {
        public List<GameObject> poolGameObjects;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            foreach (var poolGameObject in poolGameObjects)
            {
                PoolGameObjectManager.Instance.Register(poolGameObject);
            }
        }
    }
}