using System.Collections.Generic;
using UnityEngine;

namespace ColliderTool
{
    public class ExportTool : ColliderEditorTool
    {
        internal override void OnUpdate()
        {
#if UNITY_EDITOR
            ExportPrefab(); //这一步中一些操作可以放到线程中 现在会把游戏卡在这一帧
#endif
        }


#if UNITY_EDITOR

        private struct GridRecord
        {
            public Vector3Int pos;
            public Vector3 size;
            public float y;
        }

        public void ExportPrefab()
        {
            // 生成一个个的Bounds
            List<List<GridRecord>> filterResults = __FilterGrids__();
            List<Bounds> bounds = new List<Bounds>();
            HashSet<Vector2Int> posSet = new HashSet<Vector2Int>();
            foreach (var result in filterResults)
            {
                posSet.Clear();
                Vector3 size = result[0].size;
                float y = result[0].y;
                foreach (var gridRecord in result)
                {
                    posSet.Add(new Vector2Int(gridRecord.pos.x, gridRecord.pos.z));
                }

                var mergeResult = BoxMerger.Merge(posSet);
                foreach (var rect in mergeResult)
                {
                    Bounds bound = BoxMerger.ConvertTo(rect, size, y);
                    bounds.Add(bound);
                }
            }

            //Bounds To BoxCollider
            GameObject root = new GameObject($"ColliderRoot[{typeof(Driver)}]");
            root.transform.SetParent(null);
            foreach (var bound in bounds)
            {
                BoxCollider box = root.AddComponent<BoxCollider>();
                box.center = bound.center;
                box.size = bound.size;
            }

            //生成Prefab
            string path = UnityEditor.EditorUtility.SaveFilePanelInProject("Save Prefab", root.name, "prefab", "");
            if (!string.IsNullOrEmpty(path))
            {
                UnityEditor.PrefabUtility.SaveAsPrefabAsset(root, path);
            }
            
            UnityEngine.Object.DestroyImmediate(root);
            //切换null工具
            Driver.Instance.OnToolSelected(null);
        }

        /// <summary>
        /// 过滤网格信息，将同高度的相同大小的网格归类
        /// </summary>
        /// <returns></returns>
        private List<List<GridRecord>> __FilterGrids__()
        {
            //同高度，同大小的网格归类
            Dictionary<float, Dictionary<Vector3, int>> idxMap = new Dictionary<float, Dictionary<Vector3, int>>();
            List<List<GridRecord>> result = new List<List<GridRecord>>();
            foreach (Grid grid in plane.container)
            {
                float y = grid.center.y;
                Vector3 size = grid.size;
                if (!idxMap.ContainsKey(y))
                {
                    idxMap.Add(y, new Dictionary<Vector3, int>());
                }

                if (!idxMap[y].ContainsKey(size))
                {
                    idxMap[y].Add(size, result.Count);
                    result.Add(new List<GridRecord>());
                }

                int idx = idxMap[y][size];
                result[idx].Add(new GridRecord
                {
                    pos = grid.pos,
                    size = grid.size,
                    y = grid.center.y
                });
            }

            idxMap.Clear();
            return result;
        }
#endif
    }
}