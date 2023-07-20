using System;
using UnityEngine;

namespace OneButtonGame
{
    public class Player : MonoBehaviour
    {
        public float radius;
        public float orbitRadius;
        public Weapon weapon;


        private void Start()
        {
            OneButton.OnButtonUp += Attack;
        }


        public void Attack()
        {
            weapon.OnUse();
        }
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (weapon == null)
            {
                weapon = GetComponentInChildren<Weapon>();
            }
            radius = Mathf.Clamp(radius, 0, orbitRadius);
            weapon.transform.localPosition = new Vector3(orbitRadius, 0, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, orbitRadius);
        }
#endif
    }
}