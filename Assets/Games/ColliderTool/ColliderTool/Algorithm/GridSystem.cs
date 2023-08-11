using Unity.Mathematics;
using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 记录3D网格的信息 中心点 ，网格坐标， 包围盒大小
    /// </summary>
    public struct Grid
    {
        public Grid(Vector3 worldPos, Vector3 size)
        {
            this.size = size;
            pos = new Vector3Int(Mathf.RoundToInt(worldPos.x / size.x), Mathf.RoundToInt(worldPos.y / size.y),
                Mathf.RoundToInt(worldPos.z / size.z));
        }
        public Grid(Vector3 size, Vector3Int pos)
        {
            this.size = size;
            this.pos = pos;
        }

        public Vector3Int pos;
        public Vector3 size;
        public Vector3 halfSize => size / 2;
        public Vector3 center => new Vector3(pos.x * size.x, pos.y * size.y, pos.z * size.z);

        /// <summary>
        /// 只要在同一个网格坐标就是同一个网格
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return pos.GetHashCode();
        }
    }
}