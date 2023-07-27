using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Pokemon
{
    public class UIManager : MonoBehaviour
    {
        public Dictionary<Type, UIPanel> uiPanels = new Dictionary<Type, UIPanel>();
        public Dictionary<Type, GameObject> prefabs = new Dictionary<Type, GameObject>();
        [SerializeField] private Canvas _canvas;

        public void Register(GameObject prefab)
        {
            UIPanel panel = prefab.GetComponent<UIPanel>();
            prefabs[panel.GetType()] = prefab;
        }

        public T OpenUI<T>() where T : UIPanel
        {
            if (uiPanels.TryGetValue(typeof(T), out UIPanel panel))
            {
                panel.OnOpen();
                return panel as T;
            }

            GameObject uiObj = GameObject.Instantiate(prefabs[typeof(T)], _canvas.transform);
            T panel2 = uiObj.GetComponent<T>();
            panel2.OnCreate();

            uiPanels[panel2.GetType()] = panel2;
            
            return panel2;

        }

        public void CloseUI<T>() where T : UIPanel
        {
            throw new NotImplementedException();
        }

        public void DestroyUI<T>() where T : UIPanel
        {
            throw new NotImplementedException();
        }
    }
}