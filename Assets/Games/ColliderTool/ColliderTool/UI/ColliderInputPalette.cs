using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace ColliderTool
{
    internal class ColliderInputPalette : SceneSingleton<ColliderInputPalette>, IColliderToolInput
    {
        public event Action<ColliderEditorTool> OnToolSelected;
        private List<ColliderEditorTool> _tools;
        private List<KeyCode> _bindKeyCodes;

        private void Start()
        {
            _tools = new List<ColliderEditorTool>
            {
                new ArrowTool(),
                new PaintTool(),
                new EraseTool(),
                new FillTool(),
                new ExportTool(),
                new ScanTool(),
            };
            _bindKeyCodes = new List<KeyCode>()
            {
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
                KeyCode.Alpha4,
                KeyCode.Alpha5,
                KeyCode.Alpha6,
                KeyCode.Alpha6,
                KeyCode.Alpha7,
                KeyCode.Alpha8,
                KeyCode.Alpha9,
                KeyCode.Alpha0,
            };
        }

        private void Update()
        {
            //快捷键切换工具 1 2 3 4
            for (int i = 0; i < _bindKeyCodes.Count; i++)
            {
                KeyCode bindKeyCode = _bindKeyCodes[i];
                if (i >= _tools.Count) break;
                if (Input.GetKeyDown(bindKeyCode))
                {
                    OnToolSelected?.Invoke(_tools[i]);
                }
            }
        }

        private void OnGUI()
        {
            //绘制 ToolBar 依次是 画笔工具 Pointer Paint Eraser Fill 配置工具 Vector3Int AreaSize CellSize
            float defaultWidth = 80;
            GUILayout.BeginHorizontal();
            foreach (var tool in _tools)
            {
                if (GUILayout.Button(tool.GetType().Name, GUILayout.Width(defaultWidth)))
                {
                    OnToolSelected?.Invoke(tool);
                }
            }

            GUILayout.EndHorizontal();
        }
    }
}