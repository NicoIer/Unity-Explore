using System;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class EnemyTip : MonoBehaviour, IEventListener<EnemyDie>, IEventListener<SpaceShipHitEnemy>
    {
        private void Awake()
        {
            EventManager.Register<EnemyDie>(this);
            EventManager.Register<SpaceShipHitEnemy>(this);
        }

        public void OnReceiveEvent(EnemyDie e)
        {
            GameObject promptObject = ObjectPoolManager.Get(nameof(Prompt));
            promptObject.transform.position = e.pos;
            promptObject.GetComponent<Prompt>().Print("G", Color.red);
        }

        public void OnReceiveEvent(SpaceShipHitEnemy e)
        {
            GameObject promptObject = ObjectPoolManager.Get(nameof(Prompt));
            promptObject.transform.position = e.pos;
            promptObject.GetComponent<Prompt>().Print($"-{e.damage}", Color.gray);
        }
    }
}