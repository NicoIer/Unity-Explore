using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 3D网格系统
    /// 我们以中心+cellSize来表示一个网格
    /// </summary>
    public static class GridSystem
    {
        public static Vector3Int WorldToGrid(Vector3 worldPos, Vector3 cellSize)
        {
        }

        public static Vector3 GridWorldCenter()
        {
        }

        public static Vector3 GridToWorld(Vector3Int gridPos, Vector3 cellSize)
        {
        }
    }

    public struct Grid
    {
        public Vector3Int pos;
        public Vector3 size;
        public Vector3 halfSize => size / 2;
        public Vector3 worldCenter => new Vector3(pos.x * size.x, pos.y * size.y, pos.z * size.z);
        
    }
}