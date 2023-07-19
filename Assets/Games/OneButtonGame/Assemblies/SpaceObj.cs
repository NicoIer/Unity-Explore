using System;
using UnityEngine;

namespace OneButtonGame
{
    public abstract class SpaceObj: MonoBehaviour
    {
        public float radius;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        #endif
    }
}