using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneButtonGame
{
    public class GameManager: MonoBehaviour,IEventListener<GameOver>
    {
        public List<GameObject> poolObjects;
        public void Awake()
        {
            foreach (var poolObject in poolObjects)
            {
                ObjectPoolManager.Instance.Register(poolObject,poolObject.name);
            }
            EventManager.Listen<GameOver>(this);
        }

        public void OnReceiveEvent(GameOver e)
        {
            SceneManager.LoadScene("OneButtonGame-Home");
        }
    }
}