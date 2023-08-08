using Nico;

namespace Pokemon
{
    public class CelesteWallSlideState: State<CelesteMove>
    {
        public CelesteWallSlideState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.animator.WallSlide();
        }

        public override void OnUpdate()
        {
            owner.rb.velocity = new UnityEngine.Vector2(owner.rb.velocity.x, -owner.moveParams.slideSpeed);
        }
    }
}