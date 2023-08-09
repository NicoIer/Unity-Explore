using System;
using System.Collections.Generic;
using System.Linq;
using ColliderTool;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ColliderEditor
{
    //目标是制作一个用于快捷编辑碰撞的编辑器，类似编辑Tilemap的TilePlete一样的工具
    //首先要做画笔
    public class ColliderEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

        private ObjectField _targetField;
        private MergeCollider target => _targetField.value as MergeCollider;
        private Vector3Field _vector3Field;

        [MenuItem("Tools/ColliderEditorWindow")]
        public static void ShowExample()
        {
            ColliderEditorWindow wnd = GetWindow<ColliderEditorWindow>();
            wnd.titleContent = new GUIContent("ColliderEditorWindow");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            VisualElement content = m_VisualTreeAsset.Instantiate();
            root.Add(content);
            InitParams();
            
        }

        private void Update()
        {
            _vector3Field.value = target.Grid.cellSize;
        }

        public void InitParams()
        {
            
            // ToolBar
            Toolbar toolbar = rootVisualElement.Q<Toolbar>();
            toolbar.Q<Button>("arrow").clicked += ColliderToolManager.SetTool<ArrowTool>;
            toolbar.Q<Button>("paint").clicked += ColliderToolManager.SetTool<PaintTool>;
            toolbar.Q<Button>("erase").clicked += ColliderToolManager.SetTool<EraseTool>;
            toolbar.Q<Button>("fill").clicked += ColliderToolManager.SetTool<FillTool>;

            //Target
            _targetField = rootVisualElement.Q<ObjectField>("target");
            _targetField.objectType = typeof(MergeCollider);
            if (_targetField.value == null)
            {
                _targetField.value = FindObjectOfType<MergeCollider>();
            }

            if (_targetField.value == null)
            {
                _targetField.value = new GameObject("ColliderEditorTarget").AddComponent<MergeCollider>();
            }


            //默认X-Z平面
            target.Grid.cellSwizzle = GridLayout.CellSwizzle.XZY;
            ColliderToolManager.SetTarget(target);


            _targetField.RegisterValueChangedCallback(evt => { ColliderToolManager.SetTarget(target); });

            _vector3Field = rootVisualElement.Q<Vector3Field>("v3_size");

            _vector3Field.value = target.Grid.cellSize;


            if (_vector3Field.value == Vector3.zero)
            {
                _vector3Field.value = Vector3.one;
            }

            target.Grid.cellSize = _vector3Field.value;

            _vector3Field.RegisterValueChangedCallback(evt => { target.Grid.cellSize = evt.newValue; });
        }
    }
}