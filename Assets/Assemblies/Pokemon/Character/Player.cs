using Cinemachine;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{
    public class Player: SceneSingleton<Player>
    {
        private CinemachineVirtualCamera _camera;
        protected override void Awake()
        {
            base.Awake();
            // 修改相机的以跟随玩家
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
            _camera.Follow = transform;
        }
    }
}