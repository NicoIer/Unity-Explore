using System;
using UnityEditor;
using UnityEngine;

namespace ColliderTool
{
    [CustomEditor(typeof(MergeCollider))]
    public class MergeColliderEditor: Editor
    {
        private MergeCollider Target => target as MergeCollider;

        private void OnSceneGUI()
        {
            if (ColliderToolManager.target != Target)
            {
                return;
            }
            ColliderToolManager.OnSceneGUI();
        }
    }
}