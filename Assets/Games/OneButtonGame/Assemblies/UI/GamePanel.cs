using System;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class GamePanel: MonoBehaviour,IEventListener<LevelUp>
    {
        private void Awake()
        {
            EventManager.Register<LevelUp>(this);
        }

        public void OnReceiveEvent(LevelUp e)
        {
            
        }
    }
}