using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OneButtonGame
{
    [Serializable]
    public struct SceneButtonPair
    {
        public Button button;
        public AssetReference sceneRef;
    }

    public class GameEntryPanel : MonoBehaviour
    {
        public List<SceneButtonPair> sceneButtonPairs;
        private void Start()
        {
            foreach (var sceneButtonPair in sceneButtonPairs)
            {
                sceneButtonPair.button.onClick.AddListener(() =>
                {
                    OnStartButtonClick(sceneButtonPair.sceneRef);
                });
            }
        }

        private void OnStartButtonClick(AssetReference assetRef)
        {
            Addressables.LoadSceneAsync(assetRef).WaitForCompletion();
        }
    }
}