using System;
using UnityEngine;

namespace ColliderTool
{
    [DisallowMultipleComponent]
    internal class MergeCollider : SceneSingleton<MergeCollider>
    {
        [SerializeField] private Vector3 cellSize;

        private void Update()
        {
        }

        private void OnDrawGizmos()
        {
            //拿到相机大小在视角内绘制网格

        }

        private void OnGUI()
        {
        }
    }
}