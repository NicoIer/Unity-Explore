using System;
using DG.Tweening;
using UnityEngine;

namespace OneButtonGame
{
    public class Arrow : MonoBehaviour
    {
        public float timer = 0;
        private void Update()
        {
            Vector3 targetPosition = TargetPosition.Instance.transform.position;
            //朝向TargetPosition
            Vector3 vec = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            timer+=Time.deltaTime;
            if (timer > 1)
            {
                timer -= 1;
                transform.DOScale(1.2f, 0.5f);
                transform.DOScale(0.8f, 0.5f).SetDelay(0.5f);
            }
        }
    }
}