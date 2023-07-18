using System;
using DG.Tweening;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class SpaceRotate : MonoBehaviour
    {
        public float pauseTime = 0;
        public bool isPause = false;
        public float growRate = 0.02f;
        public Vector3 startScale;
        public ParticleSystem particleSystem;
        public float currentAngel = 0;
        private void Awake()
        {
            startScale = transform.localScale;
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }


        public void Pause()
        {
            isPause = true;
            pauseTime = Time.time;
            
            //修改粒子系统的旋转
            particleSystem.transform.localRotation = Quaternion.Euler(0,0,currentAngel);
            particleSystem.Play();
        }

        private void Update()
        {
            if (isPause)
            {
                transform.localScale += startScale * ((Time.time - pauseTime) * growRate);
            }
        }

        public Vector2 GetVelocity(float multiple)
        {
            return -transform.localPosition.normalized * (multiple * (Time.time - pauseTime));
        }

        public void Rotate(float currentAngel, float radius)
        {
            this.currentAngel = currentAngel;
            float x = Mathf.Cos(currentAngel/180*Mathf.PI) * radius;
            float y = Mathf.Sin(currentAngel/180*Mathf.PI) * radius;
            transform.localPosition = new Vector3(x, y, 0);
        }

        public void Release()
        {
            particleSystem.Stop();
            particleSystem.Clear();
            isPause = false;
            transform.localScale = startScale;
            pauseTime = 0;
            //生成一个云朵 从粒子系统的位置飞出来
            GameObject cloud = ObjectPoolManager.Get("Cloud");
            var position = transform.position;
            cloud.transform.position = position;
            Vector3 targetPos = position.RandomXYOffset(15);
            cloud.transform.DOMove(targetPos, 2f);
        }
    }
}