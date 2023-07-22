using System;
using UnityEngine;

namespace OneButtonGame
{
    public class CircleAttackComponent : MonoBehaviour, IPoolGameObject
    {
        private bool _isAttack;
        public CircleAttackInfo attackInfo;
        public float currentAngel;

        public TrailRenderer trailRenderer;

        private void Awake()
        {
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }


        /// <summary>
        /// 围绕center旋转，半径为radius，角度为angle，速度为speed，伤害为damage
        /// </summary>
        public void Attack(CircleAttackInfo attackInfo)
        {
            _isAttack = true;
            this.attackInfo = attackInfo;
            this.currentAngel = attackInfo.startAngel;

            transform.localScale = attackInfo.center.localScale * 0.4f;
            UpdatePosition(currentAngel);
            transform.SetParent(this.attackInfo.center);
        }


        private void Update()
        {
            if (!_isAttack) return;
            currentAngel += Time.deltaTime * attackInfo.angelSpeed;
            UpdatePosition(currentAngel);
            //攻击结束
            if (currentAngel >= attackInfo.targetAngel)
            {
                _isAttack = false;
                PoolGameObjectManager.Instance.Return(this);
            }
        }

        private void UpdatePosition(float angel)
        {
            float degree = angel / 180 * Mathf.PI;
            Vector3 vec = new Vector3(Mathf.Cos(degree), Mathf.Sin(degree), 0) *
                          attackInfo.radius;
            transform.position = attackInfo.center.position + vec;
        }


        #region PoolGameObj

        PoolObjectState IPoolObject.state { get; set; }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public void OnRecycle()
        {
            gameObject.SetActive(false);
            _isAttack = false;
            transform.position = PositionConst.HidePosition;
            transform.SetParent(null);
        }

        public GameObject GetGameObject() => gameObject;

        #endregion
    }
}