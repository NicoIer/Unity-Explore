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
                owner.rb.velocity = owner.input.move * owner.config.dashSpeed;
            }
            else if(owner.rb.velocity!= Vector2.zero)
            {
                owner.rb.velocity = owner.rb.velocity.normalized * owner.config.dashSpeed;
            }
            else
            {
                return;
            }
            
            
            owner.stateMachine.LockState(owner.config.dashTime);
            owner.config.enableBetterJumping = false;
            owner.rb.gravityScale = 0;
            
            DOVirtual.Float(14, 0, .8f, v => owner.rb.drag = v);
        }

        public override void OnUpdate()
        {
            if (owner.input.move != Vector2.zero)
            {
                owner.rb.velocity = owner.input.move * owner.config.dashSpeed;
            }
            else if(owner.rb.velocity!= Vector2.zero)
            {
                owner.rb.velocity = owner.rb.velocity.normalized * owner.config.dashSpeed;
            }
            else
            {
                return;
            }
        }

        public override void OnExit()
        {
            owner.config.enableBetterJumping = true;
            owner.rb.gravityScale = owner.config.gravityScale;
        }
    }
}