using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class StartPanel : MonoBehaviour
    {
        public AssetReference gameScene;
        public Button startButton;
        private void Start()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        private async void OnStartButtonClick()
        {
            SceneInstance sceneInstance = Addressables.LoadSceneAsync(gameScene).WaitForCompletion();
            await sceneInstance.ActivateAsync();
        }
    }
}