using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityToolkit;

namespace TowerDefence
{
    public interface ISetting
    {
        public void OnLoad();
        public void OnSave();
    }

    public abstract class SettingScriptableObject : ScriptableObject, ISetting
    {
        public abstract void OnLoad();
        public abstract void OnSave();
    }

    public class SettingManager : MonoSingleton<SettingManager>
    {
        [HideInInspector, SerializeField] private SerializableDictionary<Type, SettingScriptableObject> settingDict =
            new SerializableDictionary<Type, SettingScriptableObject>();

        public override bool dontDestroyOnLoad => true;

        public T Get<T>() where T : SettingScriptableObject
        {
            return (T)settingDict[typeof(T)];
        }

        protected override void OnInit()
        {
            base.OnInit();
            foreach (var kvp in settingDict)
            {
                kvp.Value.OnLoad();
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            foreach (var kvp in settingDict)
            {
                kvp.Value.OnSave();
            }
        }

#if UNITY_EDITOR
        [SerializeField] private List<SettingScriptableObject> editorSettingList = new List<SettingScriptableObject>();

        private void OnValidate()
        {
            settingDict.Clear();
            foreach (var setting in editorSettingList)
            {
                if (settingDict.ContainsKey(setting.GetType()))
                {
                    Debug.LogWarning($"{setting} has been added it will be override");
                    settingDict[setting.GetType()] = setting;
                    continue;
                }

                settingDict.Add(setting.GetType(), setting);
            }

            editorSettingList.Clear();
            foreach (var kvp in settingDict)
            {
                editorSettingList.Add(kvp.Value);
            }
        }
#endif
    }
}