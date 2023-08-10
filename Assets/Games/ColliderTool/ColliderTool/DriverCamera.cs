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
        private Vector3 intersection;
        public Vector2 drawAreaSize = new Vector2(20, 20);

        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<Camera>();
        }


#if UNITY_EDITOR
        [ExecuteAlways]
#endif
        private void Update()
        {
            HandleMove();
            HandleDraw();
            
        }

        private void HandleDraw()
        {
            //从相机向前做一条射线 找到绘制平面(y=0)
            Vector3 forward = transform.forward;
            Vector3 start = transform.position;
            float targetY = _mergeCollider.transform.position.y;
            //计算交点 y + targetY = 0
            Surface surface = new Surface(Vector3.up, -targetY);
            intersection = SpaceGeometry.LineSurfaceIntersection(start, forward, surface);
            _mergeCollider.SetCurrentCenter(intersection);
            
        }

        private void OnDrawGizmos()
        {
            //从交点开始绘制网格
            Gizmos.color = Color.red;
            Vector2Int currentGrid = WorldToGrid(intersection, _mergeCollider.cellSize);
            Gizmos.DrawSphere(intersection, 0.1f);
            
            //绘制网格 不绘制高度 只绘制平面
            Vector3 cellSize = _mergeCollider.cellSize;
            
        }
        
        public static Vector2Int WorldToGrid(Vector3 worldPos, Vector3 cellSize)
        {
            Vector2Int gridPos = new Vector2Int
            {
                x = Mathf.FloorToInt(worldPos.x / cellSize.x),
                y = Mathf.FloorToInt(worldPos.z / cellSize.z)
            };
            return gridPos;
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