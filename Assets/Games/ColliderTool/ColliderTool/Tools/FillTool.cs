using System.Collections.Generic;
using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 批量绘制工具
    /// 直接填满从 起点到终点构成的Bounds2D的所有网格
    /// </summary>
    public sealed class FillTool : ColliderEditorTool
    {
        private Grid _start;
        
        internal override void OnUpdate()
        {
            // //开始绘制 保存起点
            // if (Input.GetMouseButtonDown(0))
            // {
            //     plane.container.tmpGrids.Clear();
            //     _start = Driver.Instance.currentGrid;
            //     plane.container.tmpGrids.Add(_start);
            //     return;
            // }
            // //松开鼠标左键  将 临时数据保存
            // if (Input.GetMouseButtonUp(0))
            // {
            //     //终点
            //     Grid end = Driver.Instance.currentGrid;
            //     plane.container.tmpGrids.Add(end);
            //     //保存临时数据
            //     plane.container.SaveTmpGrids();
            //     return;
            // }
            //
            // //鼠标处于按下状态
            // if (Input.GetMouseButton(0))
            // {
            //     //当前网格
            //     Grid current = Driver.Instance.currentGrid;
            //     //如果当前网格不在临时数据中 添加到临时数据
            //     plane.container.tmpGrids.Add(current);
            // }
            //
        }
    }
}