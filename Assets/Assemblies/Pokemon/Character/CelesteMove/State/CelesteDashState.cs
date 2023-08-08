using System;
using DG.Tweening;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteDashState : State<CelesteMove>
    {
        public CelesteDashState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            if (owner.input.move != Vector2.zero)
            {
                owner.rb.velocity = owner.input.move * owner.moveParams.dashSpeed;
            }
            //没有输入时的冲刺依赖于玩家的朝向
            else
            {
                if (owner.facing == CelesteMoveFacing.Right)
                {
                    owner.rb.velocity = Vector2.right * owner.moveParams.dashSpeed;
                }
                else if (owner.facing == CelesteMoveFacing.Left)
                {
                    owner.rb.velocity = Vector2.left * owner.moveParams.dashSpeed;
                }
                else
                {
                    throw new ArgumentException($"unknown facing:{owner.facing} is not right or left");
                }
            }


            owner.stateMachine.LockState(owner.moveParams.dashTime);
            owner.moveParams.enableBetterJumping = false;
            owner.rb.gravityScale = 0;

            DOVirtual.Float(14, 0, .8f, v => owner.rb.drag = v);
            
            owner.animator.Dash();
        }

        public override void OnUpdate()
        {
            if (owner.input.move != Vector2.zero)
            {
                owner.rb.velocity = owner.input.move * owner.moveParams.dashSpeed;
            }
            else if (owner.rb.velocity != Vector2.zero)
            {
                owner.rb.velocity = owner.rb.velocity.normalized * owner.moveParams.dashSpeed;
            }
            else
            {
                return;
            }
        }

        public override void OnExit()
        {
            owner.moveParams.enableBetterJumping = true;
            owner.rb.gravityScale = owner.moveParams.gravityScale;
        }
    }
}