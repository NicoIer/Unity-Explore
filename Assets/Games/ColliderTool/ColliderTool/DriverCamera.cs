using System;
using UnityEngine;

namespace ColliderTool
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    internal class DriverCamera : SceneSingleton<DriverCamera>
    {
        private Camera _camera;
        public Camera Camera => _camera;
        private MergeCollider _mergeCollider => MergeCollider.Instance;
        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<Camera>();
        }
        

        private void Update()
        {
            HandleMove();
            //从相机向前做一条射线 找到绘制平面(y=0)
            Vector3 forward = transform.forward;
            Vector3 start = transform.position;
            float targetY = _mergeCollider.transform.position.y;
            //计算交点
        }

        private void HandleMove()
        {
            //自由控制相机的视角, 以及相机的移动
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 move3 = new Vector3(move.x, 0, move.y);
            transform.position += move3 * (Time.deltaTime * 10f);
        }
    }
}