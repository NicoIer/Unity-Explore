using UnityEngine;

namespace ColliderTool
{
    public sealed class EraseTool : ColliderEditorTool
    {
        public EraseTool()
        {
        }

        internal override void OnUpdate()
        {
            if (Input.GetMouseButton(0) && plane.container.Contains(Driver.Instance.currentGrid))
            {
                plane.container.Remove(Driver.Instance.currentGrid);
            }
        }
    }
}