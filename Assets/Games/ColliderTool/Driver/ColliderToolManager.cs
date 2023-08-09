using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ColliderTool
{
    public static class ColliderToolManager
    {
        public static MergeCollider target { get; private set; }
        private static ColliderEditorTool _currentTool;
        public static Vector3 size => target.Grid.cellSize;
        public static SceneView sceneView => UnityEditor.SceneView.currentDrawingSceneView;
        public static Camera sceneCamera => sceneView.camera;

        public static ColliderEditorTool currentTool
        {
            get => _currentTool;
            set => _currentTool = value;
        }


        public static void SetTarget(MergeCollider collider)
        {
            target = collider;
        }

        public static void SetTool<T>() where T : ColliderEditorTool, new()
        {
            _currentTool = new T();
        }

        public static void OnSceneGUI()
        {
            if (_currentTool == null)
            {
                return;
            }
            
            _currentTool.OnSceneGUI();
        }
        
    }
}