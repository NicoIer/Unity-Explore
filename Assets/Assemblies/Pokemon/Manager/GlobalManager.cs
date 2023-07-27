using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class GlobalManager : GlobalSingleton<GlobalManager>
    {
        [field: SerializeField] public UIManager uiManager { get; private set; }
        [field: SerializeField] public SceneManager sceneManager { get; private set; }
        [SerializeField] private List<GameObject> uiPanelPrefabs;

        protected override void Awake()
        {
            base.Awake();
            uiManager = GetComponentInChildren<UIManager>();
            sceneManager = GetComponentInChildren<SceneManager>();


            foreach (var uiPanelPrefab in uiPanelPrefabs)
            {
                uiManager.Register(uiPanelPrefab);
            }
        }

        private void Start()
        {
            uiManager.OpenUI<TitlePanel>();
        }
    }
}