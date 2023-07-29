using System;
using Cinemachine;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{
    public class Player: SceneSingleton<Player>
    {
        private CinemachineVirtualCamera _camera;
        private CelesteMove celesteMove;
        protected override void Awake()
        {
            base.Awake();
            celesteMove = GetComponent<CelesteMove>();
            // 修改相机的以跟随玩家
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
            _camera.Follow = transform;
            UIManager.Instance.OpenUI<InputPanel>();
        }


        private void OnGUI()
        {
            //左上角绘制当前状态
            GUI.Label(new Rect(100, 100, 200, 40), celesteMove.stateMachine.currentState.GetType().Name);
        }
    }
}