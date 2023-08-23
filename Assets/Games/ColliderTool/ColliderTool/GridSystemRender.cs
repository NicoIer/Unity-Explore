using UnityEngine;

namespace ColliderTool
{
    internal class GridSystemRender : MonoBehaviour
    {
        public Vector2Int drawAreaSize = Vector2Int.one * 5;
        public Vector3 gridSize => Driver.Instance.gridSize;
        public Grid currentGrid => Driver.Instance.currentGrid;
        public Vector3 intersection => Driver.Instance.intersection;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            //从交点开始绘制网格
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(intersection, 0.1f);

            //绘制网格 不绘制高度 只绘制平面
            float width = gridSize.x * drawAreaSize.x;
            float height = gridSize.z * drawAreaSize.y;
            Bounds bounds = new Bounds(currentGrid.center, new Vector3(width, 0, height));

            // Gizmos.DrawWireCube(bounds.center, bounds.size);
            //z轴绘制 current+1条线
            for (int current = 0; current < drawAreaSize.x + 1; current++)
            {
                float startX = bounds.min.x + current * gridSize.x;
                Gizmos.DrawLine(new Vector3(startX, currentGrid.center.y , bounds.min.z), new Vector3(startX, currentGrid.center.y, bounds.max.z));
            }

            //x轴绘制 z+1条线
            for (int z = 0; z < drawAreaSize.y + 1; z++)
            {
                float startZ = bounds.min.z + z * gridSize.z;
                Gizmos.DrawLine(new Vector3(bounds.min.x, currentGrid.center.y , startZ),
                    new Vector3(bounds.max.x, currentGrid.center.y , startZ));
            }

            //在交点处绘制一个Box
            Gizmos.DrawWireCube(currentGrid.center + new Vector3(0, currentGrid.size.y / 2, 0), currentGrid.size);

            //绘制当前已经绘制的内容
            Gizmos.color = Color.green;
            foreach (var grid in Driver.Instance.currentPlane.container)
            {
                Gizmos.DrawWireCube(grid.center + new Vector3(0, grid.size.y / 2, 0), grid.size);
            }
            Gizmos.color = Color.blue;
            //绘制临时内容
            foreach (var grid in Driver.Instance.currentPlane.container.tmpGrids)
            {
                Gizmos.DrawWireCube(grid.center + new Vector3(0, grid.size.y / 2, 0), grid.size);
            }
        }

#endif
    }
}