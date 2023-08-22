using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ColliderTool
{
    [RequireComponent(typeof(GridSystemRender))]
    [RequireComponent(typeof(FreeCamera))]
    [RequireComponent(typeof(IColliderToolInput))]
    internal class Driver : SceneSingleton<Driver>
    {
        private IColliderToolInput _input;
        private FreeCamera _freeCamera;
        private ColliderEditorTool _currentTool;
        public Vector3 intersection { get; private set; }
        public Grid currentGrid => new Grid(intersection, gridSize);
        private Surface Surface => new Surface(Vector3.up, -DrawPlane.Instance.transform.position.y);
        private GridSystemRender _render;
        public Vector3 gridSize = Vector3.one;
        public DrawPlane currentPlane => DrawPlane.Instance;


        protected override void Awake()
        {
            base.Awake();
            _input = GetComponent<IColliderToolInput>();
            _freeCamera = GetComponent<FreeCamera>();
            _render = GetComponent<GridSystemRender>();
        }

        private void OnEnable()
        {
            _input.OnToolSelected += OnToolSelected;
        }

        private void OnDisable()
        {
            _input.OnToolSelected -= OnToolSelected;
        }

        private void OnToolSelected(ColliderEditorTool tool)
        {
            _currentTool = tool;
        }

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _freeCamera.Camera.ScreenPointToRay(mousePos);
            intersection = SpaceGeometry.RaySurfaceIntersection(ray, Surface);
            if (_currentTool == null) return;
            _currentTool.OnUpdate();
        }

#if UNITY_EDITOR
        public void ExportPrefab()
        {
            Dictionary<Vector3, List<Grid>> size2Grids = __FilterGrids__();
            List<Bounds> bounds = new List<Bounds>(size2Grids.Count / 2);
            foreach (var grids in size2Grids.Values)
            {
                bounds.AddRange(__Combine__(grids));
            }
            // 通过bounds生成BoxCollider
        }

        /// <summary>
        /// 合并网格，尽可能的减少最后的网格数量
        /// </summary>
        /// <param name="grids">网格坐标->网格的dict</param>
        /// <returns></returns>
        private List<Bounds> __Combine__(List<Grid> grids)
        {
            List<Bounds> bounds = new List<Bounds>(grids.Count / 2);
            HashSet<Vector3Int> used = new HashSet<Vector3Int>(grids.Count);//已经使用过的grid的pos
            
            
            
            
            
            return bounds;
        }


        /// <summary>
        /// 过滤网格信息，将相同大小的网格归类
        /// </summary>
        /// <returns></returns>
        private Dictionary<Vector3, List<Grid>> __FilterGrids__()
        {
            Dictionary<Vector3, List<Grid>> size2Grids =
                new Dictionary<Vector3, List<Grid>>();
            foreach (Grid grid in currentPlane.container)
            {
                if (!size2Grids.ContainsKey(grid.size))
                {
                    size2Grids.Add(grid.size, new List<Grid>());
                }

                size2Grids[grid.size].Add(grid);
            }

            return size2Grids;
        }
#endif
    }
}