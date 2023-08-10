using System;
using UnityEngine;

namespace ColliderTool
{
    [DisallowMultipleComponent]
    internal class MergeCollider : SceneSingleton<MergeCollider>
    {
        [SerializeField] public Vector3 cellSize;
        private Vector3 intersection;

        private void Update()
        {
        }



        private void OnGUI()
        {
        }

        public void SetCurrentCenter(Vector3 intersection)
        {
            this.intersection = intersection;
        }
        private void OnDrawGizmos()
        {
            //拿到相机大小在视角内绘制网格
        }
        
    }
}