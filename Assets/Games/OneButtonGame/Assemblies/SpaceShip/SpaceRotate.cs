using System;
using DG.Tweening;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace OneButtonGame
{
    public class SpaceRotate : MonoBehaviour
    {
        private float _pauseTime = 0;
        private bool _isPause = false;
        public float force = 30;
        [SerializeField] private float growRate = 0.02f;
        [SerializeField] private Vector3 scaleLimit = new Vector3(8, 8, 8);
        private Vector3 _startScale;
        private ParticleSystem _particleSystem;
        private float _currentAngel = 0;
        public float radius;

        private void Awake()
        {
            _startScale = transform.localScale;
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void Pause()
        {
            _isPause = true;
            _pauseTime = Time.time;

            //修改粒子系统的旋转
            _particleSystem.transform.localRotation = Quaternion.Euler(0, 0, _currentAngel);
            _particleSystem.Play();
        }

        private void Update()
        {
            if (!_isPause) return;
            if (transform.localScale.x > scaleLimit.x || transform.localScale.y > scaleLimit.y ||
                transform.localScale.z > scaleLimit.z)
            {
                transform.localScale = scaleLimit;
            }
            else
            {
                transform.localScale += _startScale * ((Time.time - _pauseTime) * growRate);
            }
        }

        public Vector2 GetVelocity()
        {
            return -transform.localPosition.normalized * ((Time.time - _pauseTime) * force);
        }

        public void Rotate(float currentAngel, float radius)
        {
            this._currentAngel = currentAngel;
            float x = Mathf.Cos(currentAngel / 180 * Mathf.PI) * radius;
            float y = Mathf.Sin(currentAngel / 180 * Mathf.PI) * radius;
            transform.localPosition = new Vector3(x, y, 0);
        }

        public void Release()
        {
            _particleSystem.Stop();
            _particleSystem.Clear();

            ReleaseEffect();

            _isPause = false;
            transform.DOScale(_startScale, 0.5f);
            _pauseTime = 0;
        }

        private void ReleaseEffect()
        {
            //生成一个云朵
            GameObject cloudObj = ObjectPoolManager.Instance.Get(nameof(Cloud));
            cloudObj.transform.position = transform.position;
            Cloud cloud = cloudObj.GetComponent<Cloud>();
            cloud.SetVelocity(-GetVelocity());
        }

        public Vector2 GetDir()
        {
            return transform.localPosition.normalized;
        }
        

#if UNITY_EDITOR
        public void Modify(float radius)
        {
            Vector2 dir = GetDir();
            transform.localPosition = dir * radius;
        }

        private void OnDrawGizmos()
        {
            SpaceShip spaceShip = GetComponentInParent<SpaceShip>();
            if (spaceShip == null) return;
            // float roundRadius = spaceShip.orbitalRadius;
            // //从飞船 向自己 画 radius 长的线
            // Gizmos.color = Color.red;
            // Vector3 position = spaceShip.transform.position;
            // Gizmos.DrawLine(position, position + transform.localPosition.normalized * roundRadius);

            //给自己画圆
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, this.radius);
        }

        private void OnValidate()
        {
            if (_particleSystem == null)
            {
                _particleSystem = GetComponentInChildren<ParticleSystem>();
            }

            _particleSystem.transform.localScale = transform.localScale;
        }
#endif
    }
}