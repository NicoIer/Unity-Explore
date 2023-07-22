using System;
using UnityEngine;

namespace Kingdom
{
    [AddComponentMenu("Kingdom/King")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class King : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        public Vector2 speed = new Vector2(1f, 1f);

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            _rb2D.velocity = KingdomInputManager.Instance.Move() * speed;
        }
    }
}