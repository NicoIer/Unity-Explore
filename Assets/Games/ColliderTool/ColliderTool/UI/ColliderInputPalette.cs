using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace ColliderTool
{
    internal class ColliderInputPalette : SceneSingleton<ColliderInputPalette>, IColliderToolInput
    {
        public event Action<ColliderEditorTool> OnToolSelected;
        // private List<ColliderEditorTool> _tools = new List<ColliderEditorTool>();
        private ArrowTool _arrowTool;
        private PaintTool _paintTool;
        private EraseTool _eraseTool;
        private FillTool _fillTool;

        private void Start()
        {
            // _tools = new List<ColliderEditorTool>
            // {
            //     new ArrowTool(),
            //     new PaintTool(),
            //     new EraseTool(),
            //     new FillTool()
            // };
            _arrowTool = new ArrowTool();
            _paintTool = new PaintTool();
            _eraseTool = new EraseTool();
            _fillTool = new FillTool();
        }

        private void Update()
        {
            //快捷键切换工具 1 2 3 4
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnToolSelected?.Invoke(_arrowTool);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnToolSelected?.Invoke(_paintTool);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnToolSelected?.Invoke(_eraseTool);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                OnToolSelected?.Invoke(_fillTool);
                return;
            }
        }

        private void OnGUI()
        {
            //绘制 ToolBar 依次是 画笔工具 Pointer Paint Eraser Fill 配置工具 Vector3Int AreaSize CellSize
            float defaultWidth = 80;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tool", GUILayout.Width(defaultWidth));
            if (GUILayout.Button("Arrow", GUILayout.Width(defaultWidth)))
            {
                OnToolSelected?.Invoke(_arrowTool);
            }

            if (GUILayout.Button("Paint", GUILayout.Width(defaultWidth)))
            {
                OnToolSelected?.Invoke(_paintTool);
            }

            if (GUILayout.Button("Eraser", GUILayout.Width(defaultWidth)))
            {
                OnToolSelected?.Invoke(_eraseTool);
            }

            if (GUILayout.Button("Fill", GUILayout.Width(defaultWidth)))
            {
                OnToolSelected?.Invoke(_fillTool);
            }
#if UNITY_EDITOR
            if (GUILayout.Button("Export", GUILayout.Width(defaultWidth)))
            {
                Driver.Instance.ExportPrefab();
            }
#endif
            GUILayout.EndHorizontal();
        }
    }
}