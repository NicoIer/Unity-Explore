using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace ColliderEditor
{
    /// <summary>
    /// 目标平面
    /// </summary>
    public enum TargetPlane
    {
        XY,
        XZ,
        YZ
    }

    [RequireComponent(typeof(MeshFilter))]
    public class ConcaveCollider : MonoBehaviour
    {
        public TargetPlane targetPlane;
        public float height = 1;
        public MeshFilter meshFilter;

        private void OnValidate()
        {
            meshFilter = GetComponent<MeshFilter>();
        }
    }


    [CustomEditor(typeof(ConcaveCollider))]
    public class ConcaveColliderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //绘制一个按钮 "Simpliy"
            if (GUILayout.Button("Simplify"))
            {
                if (target is not ConcaveCollider collider)
                {
                    return;
                }

                if (collider.meshFilter.sharedMesh == null)
                {
                    return;
                }
                //获取当前编辑对象的Mesh的顶点
                Vector3[] vertices = collider.meshFilter.sharedMesh.vertices;
                

            }
        }
    }
}
#endif