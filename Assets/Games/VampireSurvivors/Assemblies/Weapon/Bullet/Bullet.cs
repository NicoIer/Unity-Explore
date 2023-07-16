using System;
using Nico;
using UnityEngine;

namespace VampireSurvivors
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public BulletSetting setting { get; private set; }
        private float _currentLifeTime;
        private Transform tar;
        private float currentSpeed;

        public void Init(Transform tar, float extraSpeed = 0)
        {
            this.tar = tar;
            this.currentSpeed += extraSpeed;
        }

        private void OnEnable()
        {
            this.currentSpeed = setting.speed;
            _currentLifeTime = 0;
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            //fly to tar
            Vector3 dir = tar.position - transform.position;
            if (dir.magnitude < 0.5f)
            {
                ObjectPoolManager.Return(gameObject);
                return;
            }
            
            transform.position += dir.normalized * (currentSpeed * Time.deltaTime);
            // life time to die
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime > setting.lifeTime)
            {
                _currentLifeTime = 0;
                ObjectPoolManager.Return(gameObject);
            }
        }
    }
}