using System;
using DG.Tweening;
using Nico;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OneButtonGame
{
    public class SpaceRotate : MonoBehaviour
    {
        private float _pauseTime = 0;
        private bool _isPause = false;
        [SerializeField] private float growRate = 0.02f;
        [SerializeField] private Vector3 scaleLimit = new Vector3(8, 8, 8);
        private Vector3 _startScale;
        private ParticleSystem _particleSystem;
        private float _currentAngel = 0;

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

        public Vector2 GetVelocity(float multiple)
        {
            return -transform.localPosition.normalized * (multiple * (Time.time - _pauseTime));
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

            //生成一个云朵
            GameObject cloudObj = ObjectPoolManager.Get(nameof(Cloud));
            cloudObj.transform.position = transform.position;
            Cloud cloud = cloudObj.GetComponent<Cloud>();
            cloud.SetVelocity(-GetVelocity(Random.Range(3, 5)));
            
            _isPause = false;
            transform.DOScale(_startScale, 0.5f);
            _pauseTime = 0;
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
#endif
    }
}