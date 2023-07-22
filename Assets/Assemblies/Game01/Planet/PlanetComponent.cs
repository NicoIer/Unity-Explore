using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OneButtonGame
{
    [AddComponentMenu("")]
    public abstract class PlanetComponent : MonoBehaviour
    {
        [FoldoutGroup("Planet")] public PlanetComponent orbitTarget;
        [FoldoutGroup("Planet")] public float currentSelfAngel = 0;
        [FoldoutGroup("Planet")] public float selfRadius = 1;
        [FoldoutGroup("Planet")] public float rotateSpeed = 60;

        [FoldoutGroup("Planet")] public float currentOrbitAngel = 0;
        [FoldoutGroup("Planet")] public float orbitRadius = 1;
        [FoldoutGroup("Planet")] public float orbitSpeed = 60;


        public virtual void OnSelfRotate()
        {
            //自转 Z 轴
            currentSelfAngel += rotateSpeed * Time.deltaTime;
            if (currentSelfAngel > 360) currentSelfAngel -= 360;
            float currentAngelDegree = currentSelfAngel; // 180 * Mathf.PI;
            transform.localRotation = Quaternion.Euler(0, 0, currentAngelDegree);
        }

        public virtual void OnOrbitRotate()
        {
            //公转 X 轴
            if (orbitTarget == null) return;
            currentOrbitAngel += orbitSpeed * Time.deltaTime;
            if (currentOrbitAngel > 360) currentOrbitAngel -= 360;
            float currentAngelDegree = currentOrbitAngel / 180 * Mathf.PI;
            Vector3 vec = new Vector3(Mathf.Cos(currentAngelDegree), Mathf.Sin(currentAngelDegree), 0) *
                          orbitRadius;
            transform.position = orbitTarget.transform.position + vec;
        }

        protected virtual void Update()
        {
            if (rotateSpeed > 0)
            {
                OnSelfRotate();
            }

            if (orbitSpeed > 0)
            {
                OnOrbitRotate();
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            //根据当前的z轴旋转角度，计算出自己自传角度
            currentSelfAngel = transform.localRotation.eulerAngles.z;

            if (orbitTarget == null) return;
            //自己到目标的向量
            Vector3 dir = (transform.position - orbitTarget.transform.position).normalized;
            //自己到目标的角度
            currentOrbitAngel = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
            //自己到目标的距离必须要等于公转半径
            Vector3 vec = new Vector3(dir.x, dir.y, 0) * orbitRadius;
            transform.position = orbitTarget.transform.position + vec;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, selfRadius);

            if (orbitTarget != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(orbitTarget.transform.position, orbitRadius);
            }
        }
#endif
    }
}