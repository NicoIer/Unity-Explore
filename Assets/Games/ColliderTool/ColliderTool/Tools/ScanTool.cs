using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColliderTool
{
    public class ScanTool : ColliderEditorTool
    {
        private Vector3Int _startPos;
        private Vector3Int _endPos;
        private CancellationTokenSource _cts;

        private HashSet<Grid> _createdGrids = new HashSet<Grid>(500);


        internal override async void OnUpdate()
        {
            if (_cts != null) return; //已经在扫描了

            _cts = new CancellationTokenSource();
            //等待玩家选起点和终点
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _cts.Token);
            _startPos = Driver.Instance.currentGrid.pos;
            plane.container.AddTemp(_startPos);
            // 等一帧 避免读取同一个输入
            await UniTask.Yield();

            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _cts.Token);
            _endPos = Driver.Instance.currentGrid.pos;
            plane.container.AddTemp(_endPos);

            //开启扫描任务
            await ScanAndCreate(_cts.Token); // 这个必须在主线程做
            //完成操作
            plane.container.ClearTemp();
            Driver.Instance.OnToolSelected(null);
        }

        internal async UniTask ScanAndCreate(CancellationToken token)
        {
            if (_startPos == _endPos || _startPos.y != _endPos.y) return;
            _createdGrids.Clear();
            //遍历场景内的所有物体 找到所有的 MeshFilter 中的mesh 中的顶点信息
            MeshFilter[] filters = Object.FindObjectsOfType<MeshFilter>(false); //这是究极耗时的操作
            //遍历所有的顶点信息，找到所有的顶点信息 这里可以并行
            foreach (var filter in filters)
            {
                if (token.IsCancellationRequested) return;
                Mesh mesh = filter.sharedMesh;
                if (mesh == null) continue;
                Vector3[] vertices = mesh.vertices;
                foreach (var vertex in vertices)
                {
                    if (token.IsCancellationRequested) return;
                    Vector3 worldPos = filter.transform.TransformPoint(vertex); //这里的顶点信息是世界坐标

                    Grid grid = new Grid(worldPos, Driver.Instance.gridSize);
                    //判断这个点是否在起点和终点之间
                    Vector3Int gridPos = grid.pos;
                    if (gridPos.x >= _startPos.x && gridPos.x <= _endPos.x &&
                        gridPos.y >= _startPos.y && gridPos.y <= _endPos.y &&
                        gridPos.z >= _startPos.z && gridPos.z <= _endPos.z)
                    {
                        //这个点在起点和终点之间
                        //判断这个点是否在已经扫描过的点中
                        if (_createdGrids.Contains(grid)) continue;
                        //这个点没有扫描过
                        _createdGrids.Add(grid);
                    }
                }
            }

            //这一步可以在子线程做
            foreach (var grid in _createdGrids)
            {
                plane.container.Add(grid);
            }

            _createdGrids.Clear();

            await UniTask.Yield();
        }


        public override void OnDisable()
        {
            _cts?.Cancel();
            _cts = null;
        }
    }
}