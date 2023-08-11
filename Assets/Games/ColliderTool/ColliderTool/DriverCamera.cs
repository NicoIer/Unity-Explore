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
        private Vector3 cellSize => _mergeCollider.cellSize;
        private Vector3 intersection;
        public Vector2 drawAreaSize = new Vector2(20, 20);

        private Surface surface => new Surface(Vector3.up, -_mergeCollider.transform.position.y);

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
            UpdatePosition();
        }
        
        private void UpdatePosition()
        {
            //绘制鼠标位置的网格
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePos);
            intersection = SpaceGeometry.RaySurfaceIntersection(ray, surface);
            float rayDistance = Vector3.Distance(ray.origin, intersection);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
            _mergeCollider.SetCurrentCenter(intersection);
        }

        private void HandleMove()
        {
            //自由控制相机的视角, 以及相机的移动
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 move3 = new Vector3(move.x, 0, move.y);
            transform.position += move3 * (Time.deltaTime * 10f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            //从交点开始绘制网格
            Gizmos.color = Color.red;

            Grid center = new Grid(intersection, _mergeCollider.cellSize);
            Gizmos.DrawSphere(intersection, 0.1f);

            //绘制网格 不绘制高度 只绘制平面
            float width = cellSize.x * drawAreaSize.x;
            float height = cellSize.z * drawAreaSize.y;
            Bounds bounds = new Bounds(center.worldCenter, new Vector3(width, 0, height));

            // Gizmos.DrawWireCube(bounds.center, bounds.size);
            //x轴绘制 x+1条线
            for (int x = 0; x < drawAreaSize.x + 1; x++)
            {
                float startX = bounds.min.x + x * cellSize.x;
                Gizmos.DrawLine(new Vector3(startX, 0, bounds.min.z), new Vector3(startX, 0, bounds.max.z));
            }

            //z轴绘制 z+1条线
            for (int z = 0; z < drawAreaSize.y + 1; z++)
            {
                float startZ = bounds.min.z + z * cellSize.z;
                Gizmos.DrawLine(new Vector3(bounds.min.x, 0, startZ), new Vector3(bounds.max.x, 0, startZ));
            }
        }
#endif
    }
}