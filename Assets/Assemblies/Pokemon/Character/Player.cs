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
        private CinemachineVirtualCamera _camera;
        private CelesteMove celesteMove;
         
        //后面替换出去
        public ParticleSystem dust;
        public VisualEffect snow;

        protected override void Awake()
        {
            base.Awake();
            celesteMove = GetComponent<CelesteMove>();
            // 修改相机的以跟随玩家
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
            _camera.Follow = transform;
            UIManager.Instance.OpenUI<InputPanel>();
            celesteMove.stateMachine.OnStateChanged += OnStateChanged;
            
            snow.Play();
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
        }

        private void OnGUI()
        {
            //左上角绘制当前状态
            GUI.Label(new Rect(100, 100, 200, 40), celesteMove.stateMachine.currentState.GetType().Name);
        }
    }
}