using System;
using System.Threading;
using UnityEngine;

namespace OneButtonGame
{
    public class Weapon : MonoBehaviour
    {
        public float radius;
        public float rotateRadius;
        public float currentAngel;
        public float angelSpeed;
        public bool isUsing;
        public float timer;

        public void OnUse()
        {
            if (isUsing)
            {
                return;
            }

            //绕着玩家旋转
            Vector2 dir = transform.localPosition.normalized;
            currentAngel = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
            isUsing = true;
        }

        public void SetRotateRadius(float radius)
        {
            rotateRadius = radius;
        }

        private void Update()
        {
            if (!isUsing) return;
            currentAngel += angelSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            float currentAngelDegree = currentAngel / 180 * Mathf.PI;
            transform.localPosition =
                new Vector3(Mathf.Cos(currentAngelDegree), Mathf.Sin(currentAngelDegree), 0) * rotateRadius;
            if (timer > 1)
            {
                isUsing = false;
                timer = 0;
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}