using System;
using Nico;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace OneButtonGame
{
    public class GlobalManager : GlobalSingleton<GlobalManager>
    {
        [field: SerializeField] public AssetReference titleScene { get; private set; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            Debug.Log("GlobalManager Start");
        }

        public void BackToTitle()
        {
            Addressables.LoadSceneAsync(titleScene);
        }
    }
}