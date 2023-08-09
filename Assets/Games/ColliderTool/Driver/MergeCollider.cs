using System;
using UnityEngine;

namespace ColliderTool
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Grid))]
    public class MergeCollider: MonoBehaviour
    {
        private Grid _grid;
        public Grid Grid
        {
            get
            {
                if(_grid== null)
                {
                    _grid = GetComponent<Grid>();
                }

                return _grid;
            }
        }

        private void OnDrawGizmos()
        {
            if(ColliderToolManager.target!=this)
            {
                return;
            }
        }

        private void Update()
        {
        }
    }
}