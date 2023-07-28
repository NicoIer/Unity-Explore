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
            else if(owner.rb.velocity!= Vector2.zero)
            {
                owner.rb.velocity = owner.rb.velocity.normalized * owner.moveParams.dashSpeed;
            }
            else
            {
                return;
            }
            
            
            owner.stateMachine.LockState(owner.moveParams.dashTime);
            owner.moveParams.enableBetterJumping = false;
            owner.rb.gravityScale = 0;
            
            DOVirtual.Float(14, 0, .8f, v => owner.rb.drag = v);
        }

        public override void OnUpdate()
        {
            if (owner.input.move != Vector2.zero)
            {
                owner.rb.velocity = owner.input.move * owner.moveParams.dashSpeed;
            }
            else if(owner.rb.velocity!= Vector2.zero)
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