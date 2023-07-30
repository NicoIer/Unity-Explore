using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{
    public interface ICelesteMoveAnimator
    {
        public void Jump();
        public void Walk();
        public void Idle();
        public void Dash();
        public void Down();
        public void Up();
        public void WallSlide();
        public void WallJump();
        public void Climb();
    }

    [RequireComponent(typeof(Animator))]
    public class CelesteMoveAnimator : MonoBehaviour, ICelesteMoveAnimator
    {
        private Animator animator;
        public string idleName = "Idle";
        public string walkName = "Walk";
        public string jumpName = "Jump";
        public string dashName = "Dash";
        public string downName = "Down";
        public string upName = "Up";

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        public void Jump()
        {
            animator.Play(idleName, 0, 0);
        }

        public void Walk()
        {
            animator.Play(walkName, 0, 0);
        }

        public void Idle()
        {
            animator.Play(jumpName, 0, 0);
        }

        public void Dash()
        {
            animator.Play(dashName, 0, 0);
        }

        public void Down()
        {
            animator.Play(downName, 0, 0);
        }

        public void Up()
        {
            animator.Play(upName, 0, 0);
        }

        public void WallSlide()
        {
        }

        public void WallJump()
        {
        }

        public void Climb()
        {
        }
    }
}