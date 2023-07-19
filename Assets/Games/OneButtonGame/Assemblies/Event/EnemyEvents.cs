using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public struct EnemyHitSpaceShip: IEvent
    {
        public Vector3 pos;
        public float hitPower;
        public float speed;
    }
    
    public struct SpaceShipHitEnemy: IEvent
    {
        public Vector3 pos;
        public int damage;
        public Enemy enemy;
    }
    public struct EnemyDie: IEvent
    {
        public Vector3 pos;
        public Enemy enemy;
    }
}