using System;
using UnityEngine;

namespace OneButtonGame
{
    [Serializable]
    public struct CircleAttackInfo
    {
        public Transform center;
        public float radius;
        public float startAngel;
        public float targetAngel;
        public float angelSpeed;
        public float damage;
    }
}