using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class GlobalManager : GlobalSingleton<GlobalManager>
    {
        [field: SerializeField] public SceneManager sceneManager { get; private set; }
        [SerializeField] private List<GameObject> uiPanelPrefabs;

        protected override void Awake()
        {
            base.Awake();
            sceneManager = GetComponentInChildren<SceneManager>();


            foreach (var uiPanelPrefab in uiPanelPrefabs)
            {
                UIManager.Instance.Register(uiPanelPrefab);
            }
        }

        private void Start()
        {
            UIManager.Instance.OpenUI<TitlePanel>();
        }
    }
}