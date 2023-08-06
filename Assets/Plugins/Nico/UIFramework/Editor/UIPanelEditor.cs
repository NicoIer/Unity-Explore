using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Nico.Editor
{
    [CustomEditor(typeof(UIPanel),true)]
    public  class UIPanelEditor: UnityEditor.Editor
    {
        public TextAsset CodeTemplate;
        public List<Type> buildInComponents=new List<Type>()
        {
            typeof(Button),
        };
        public override void OnInspectorGUI()
        {
            //这样会把Odin的序列化取消掉，变得很丑
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate Code"))
            {
                GenerateCode(target as UIPanel);
            }
        }

        private void GenerateCode(UIPanel uiPanel)
        {
            Type type = uiPanel.GetType();
            //判断这个类是不是partial class
            string name = type.Name;
            //找到这个类的定义文件的位置
            string path = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(uiPanel));
            Debug.Log(path);
            //同目录下生成 partial 代码
            string code = __generatePartialCode(uiPanel);
        }

        private string __generatePartialCode(UIPanel panel)
        {
            throw new NotImplementedException();
        }

        private string _GetComponentFieldStr()
        {
            throw new NotImplementedException();
        }

        private string _GetComponentBindingStr()
        {
            throw new NotImplementedException();
        }
    }
}