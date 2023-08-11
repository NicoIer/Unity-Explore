using Unity.Mathematics;
using UnityEngine;

namespace ColliderTool
{
    public struct Grid
    {
        public Grid(Vector3 worldPos, Vector3 size)
        {
            this.size = size;
            pos = new Vector3Int(Mathf.FloorToInt(worldPos.x / size.x), Mathf.FloorToInt(worldPos.y / size.y),
                Mathf.FloorToInt(worldPos.z / size.z));
        }
        public Grid(Vector3 size, Vector3Int pos)
        {
            this.size = size;
            this.pos = pos;
        }

        public Vector3Int pos;
        public Vector3 size;
        public Vector3 halfSize => size / 2;
        public Vector3 worldCenter => new Vector3(pos.x * size.x, pos.y * size.y, pos.z * size.z);
    }
}