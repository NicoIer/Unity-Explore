using System;
using UnityEngine;

namespace Pokemon
{
    public interface ICelesteCollider
    {
        public bool isGrounded { get; }
        public bool isTouchingWall { get; }
        public bool isTouchingWallRight { get; }
        public bool isTouchingWallLeft { get; }
    }

    public class CelesteCollider : MonoBehaviour, ICelesteCollider
    {
        public bool isGrounded { get; set; }
        public bool isTouchingWall { get; set; }
        public bool isTouchingWallRight { get; set; }
        public bool isTouchingWallLeft { get; set; }
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private Vector3 footOffset;
        [SerializeField] private float footRadius = 0.1f;
        [SerializeField] private Vector3 rightOffset;
        [SerializeField] private Vector3 leftOffset;
        [SerializeField] private float handRadius = 0.1f;

        private void Update()
        {
            Vector3 position = transform.position;
            isGrounded = Physics2D.OverlapCircle(position + footOffset, footRadius, groundLayer);
            isTouchingWallLeft = Physics2D.OverlapCircle(position + leftOffset, handRadius, wallLayer);
            isTouchingWallRight = Physics2D.OverlapCircle(position + rightOffset, handRadius, wallLayer);
            isTouchingWall = isTouchingWallLeft || isTouchingWallRight;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Gizmos.DrawWireSphere(position + footOffset, footRadius);
            Gizmos.DrawWireSphere(position + rightOffset, handRadius);
            Gizmos.DrawWireSphere(position + leftOffset, handRadius);
        }
#endif
    }
}