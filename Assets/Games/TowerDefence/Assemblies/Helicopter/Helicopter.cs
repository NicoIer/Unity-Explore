using System;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(Rigidbody))]
    public class Helicopter: MonoBehaviour
    {
        public Rigidbody rb;
        //ROTATE
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {

        }
        private void FixedUpdate()
        {

        }
    }
}