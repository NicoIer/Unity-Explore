using System;
using Nico.VirtualControl;
using UnityEngine;

namespace OneFingerDeathPunch
{
    public class VirtualStickListener: MonoBehaviour
    {
        public VirtualStick stick;
        public float speed = 5f;

        public void Update()
        {
            Vector2 move = stick.ReadAxis();
            transform.position += new Vector3(move.x, move.y, 0) * (Time.deltaTime * speed);
        }
    }
}