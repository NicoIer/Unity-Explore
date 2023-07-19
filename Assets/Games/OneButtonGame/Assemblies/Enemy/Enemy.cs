using System;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneButtonGame
{
    public class Enemy : MonoBehaviour
    {
        public SpaceShip spaceShip => SpaceShip.Instance;
        public float defaultSpeed = 2;
        public float enableTime;
        public float hitPower = 5;
        public float returnTime = 12;
        public float radius = 1;
        public int damage = 1;

        private void OnEnable()
        {
            enableTime = Time.time;
        }


        public void FixedUpdate()
        {
            Vector3 position = transform.position;
            //向飞船移动
            float speed = GetSpeed();
            transform.MoveTo(spaceShip.transform, speed, Time.deltaTime);
            //被飞船攻击
            if (Vector3.Distance(position, spaceShip.spaceRotate.transform.position) <
                spaceShip.spaceRotate.radius + radius)
            {
                EventManager.Send<SpaceShipHitEnemy>(
                    new SpaceShipHitEnemy()
                    {
                        pos = position,
                        damage = damage,
                        enemy = this
                    });
                EventManager.Send<EnemyDie>(new EnemyDie()
                {
                    pos = position,
                    enemy = this
                });
                ObjectPoolManager.Return(gameObject);
                return;
            }

            //撞击到飞船
            if (Vector3.Distance(position, spaceShip.transform.position) < spaceShip.radius + radius)
            {
                EventManager.Send<EnemyHitSpaceShip>(new EnemyHitSpaceShip()
                {
                    pos = transform.position,
                    hitPower = hitPower,
                    speed = speed
                });
                PlayerModelController.Damage(damage);
                ObjectPoolManager.Return(gameObject);
                return;
            }

            //超出存活时间
            if (returnTime < Time.time - enableTime)
            {
                ObjectPoolManager.Return(gameObject);
                return;
            }
        }


        public float GetSpeed()
        {
            return defaultSpeed + 0.5f * spaceShip.velocity.magnitude * (Time.time - enableTime) /
                Time.time;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Transform self = transform;
            var position = self.position;
            //画出自己的半径
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, radius);
            
            
            if (!Application.isPlaying) return;
            //画出自己到飞船的距离线
            Gizmos.color = Color.red;
            Transform tar = spaceShip.transform;
            Vector3 vec = (tar.position - position);
            Vector3 dir = vec.normalized;
            float distance = vec.magnitude - spaceShip.radius;
            Vector3 end = position + dir * distance;

            Gizmos.DrawLine(position, end);


        }
#endif
    }
}