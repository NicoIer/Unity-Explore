using UnityEngine;

namespace ColliderTool
{
    public sealed class PaintTool : ColliderEditorTool
    {
        internal override void OnUpdate()
        {
            //左键点击 & !当前绘制平面包含当前网格
            if (Input.GetMouseButton(0)&& !plane.container.Contains(Driver.Instance.currentGrid))
            {
                plane.container.Add(Driver.Instance.currentGrid);
            }
        }
    }
}