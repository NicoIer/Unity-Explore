using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Pokemon
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private AssetReference titleScene;
        [SerializeField] private AssetReference loadingScene;
        [SerializeField] private AssetReference homeScene;
        [SerializeField] private AssetReference gameScene;

        public async UniTask ToTitle()
        {
            await Addressables.LoadSceneAsync(titleScene);
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<TitlePanel>();
        }

        public async UniTask ToHome()
        {
            await Addressables.LoadSceneAsync(homeScene);
        }

        public async UniTask ToGame()
        {
            await Addressables.LoadSceneAsync(gameScene);
        }
    }
}