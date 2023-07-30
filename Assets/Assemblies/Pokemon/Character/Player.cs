using System;
using Cinemachine;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Pokemon
{
    public class Player : SceneSingleton<Player>
    {
        [SerializeField]private CinemachineVirtualCamera _camera;
        private CelesteMove celesteMove;
        private SpriteRenderer spriteRenderer;
        public CelesteMoveFacing facing => celesteMove.facing;

        //后面替换出去
        public ParticleSystem dust;
        public VisualEffect snow;
        public DashEffect dashEffect;

        protected override void Awake()
        {
            base.Awake();
            celesteMove = GetComponent<CelesteMove>();
            // 修改相机的以跟随玩家
            _camera.Follow = transform;
            
            UIManager.Instance.OpenUI<InputPanel>();
        }

        private void Start()
        {
            celesteMove.stateMachine.OnStateChanged += OnStateChanged;
            celesteMove.OnFacingChange += OnFacingChange;
            // anim
            spriteRenderer = GetComponent<SpriteRenderer>();
            // Effects 
            snow.Play();
            dashEffect.SetTarget(spriteRenderer);
        }

        private void OnFacingChange(CelesteMoveFacing obj)
        {
            spriteRenderer.flipX = obj == CelesteMoveFacing.Left;
        }

        private void OnStateChanged(State<CelesteMove> pre, State<CelesteMove> now)
        {
            if (now is CelesteJumpState)
            {
                dust.Play();
            }

            if (pre is CelesteDownState && (now is CelesteIdleState || now is CelesteWalkState))
            {
                dust.Play();
            }

            // 冲刺
            if (now is CelesteDashState)
            {
                dashEffect.Play();
            }

            if (pre is CelesteDashState)
            {
                dashEffect.Stop();
            }
        }

        private void OnGUI()
        {
            //左上角绘制当前状态
            GUI.Label(new Rect(100, 100, 200, 40), celesteMove.stateMachine.currentState.GetType().Name);
        }
    }
}