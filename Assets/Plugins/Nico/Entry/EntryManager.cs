using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using HybridCLR;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

namespace Entry
{
    public class EntryManager : MonoBehaviour
    {
        public AssetReference mainScene;
        public AssetLabelReference dllLabel;
        public AssetLabelReference aotLabel;

        public event Action<float> progressChanged;
        public Button startButton;

        public void Start()
        {
            progressChanged?.Invoke(0);
            //将下载资源的服务器地址设置为自己的服务器地址
            _check_update().Forget();
        }

        private async UniTask _check_update()
        {
            //检查资源更新任务
            await _update_address_ables();
            //加载AOT元数据DLL任务
            await _load_meta_data_for_aot_dlls();
            //加载热更DLL任务
            await _load_hotfix_dlls();

            await _enter_hotfix_main_scene();
        }

        private async UniTask _update_address_ables()
        {
            // 初始化Addressables
            await Addressables.InitializeAsync();

            // 检查文件更新
            // 这一步会根据Addressables中的资源组来依次检查更新
            // 打包后 会 从配置中的RemoteBuildPath中下载资源
            // Addressables 会自动根据catalog中各个资源的hash值来判断是否需要更新
            List<string> catalogs = await Addressables.CheckForCatalogUpdates().Task;
            if (catalogs.Count <= 0)
            {
                progressChanged?.Invoke(0.5f);
                return;
            }

            //需要更新资源  则 根据catalogs 拿到需要更新的资源位置 
            List<IResourceLocator> resourceLocators = await Addressables.UpdateCatalogs(catalogs).Task;
            // Debug.Log($"需要更新:{resourceLocators.Count}个资源");
            int count = 0;
            foreach (IResourceLocator resourceLocator in resourceLocators)
            {
                // Debug.Log($"开始下载资源:{resourceLocator.Keys}");
                await _download(resourceLocator);
                // Debug.Log($"下载资源:{resourceLocator}完成");
                ++count;
                progressChanged?.Invoke(0.5f + count / (float)resourceLocators.Count * 0.3f);
            }
        }

        private async UniTask _download(IResourceLocator resourceLocator)
        {
            long size = await Addressables.GetDownloadSizeAsync(resourceLocator.Keys).Task;
            // Debug.Log($"更新:{resourceLocator}资源,总大小:{size}");
            if (size <= 0) return;
            var downloadHandle =
                Addressables.DownloadDependenciesAsync(resourceLocator.Keys, Addressables.MergeMode.Union);
            // float progress = 0;
            // while (downloadHandle.Status == AsyncOperationStatus.None)
            // {
            // float percentageComplete = downloadHandle.GetDownloadStatus().Percent;
            // if (percentageComplete > progress * 1.01) // Report at most every 10% or so
            // {
            //     progress = percentageComplete; // More accurate %
            //     print($"下载百分比：{progress * 100}%");
            // }

            // await UniTask.WaitForFixedUpdate();
            // }

            await downloadHandle;

            // Debug.Log("更新完毕!");
            Addressables.Release(downloadHandle);
        }

        private async UniTask _load_hotfix_dlls()
        {
            // 加载热更DLL
            // 这里使用标签来加载资源 Addressables会自动根据标签来加载所有资源
            IList<TextAsset> dlls = await Addressables.LoadAssetsAsync<TextAsset>(dllLabel, null).Task;
            if (dlls == null)
            {
                Debug.Log("没有需要加载的热更DLL");
                return;
            }

            int count = 0;
            foreach (var asset in dlls)
            {
                Assembly.Load(asset.bytes);
                ++count;
                progressChanged?.Invoke(0.8f + 0.1f * count / dlls.Count);
            }
        }

        private async UniTask _load_meta_data_for_aot_dlls()
        {
            //这一步实际上是为了解决AOT 泛型类的问题 
            HomologousImageMode mode = HomologousImageMode.SuperSet;
            IList<TextAsset> aots = await Addressables.LoadAssetsAsync<TextAsset>(aotLabel, null).Task;
            if (aots == null)
            {
                progressChanged?.Invoke(1);
                // Debug.Log("没有需要加载的AOT元数据DLL");
                return;
            }

            int count = 0;
            foreach (var asset in aots)
            {
                LoadImageErrorCode errorCode = RuntimeApi.LoadMetadataForAOTAssembly(asset.bytes, mode);
                if (errorCode == LoadImageErrorCode.OK)
                {
                    continue;
                }

                ++count;
                Debug.LogError($"加载AOT元数据DLL:{asset.name}失败,错误码:{errorCode}");
                progressChanged?.Invoke((float)(0.9 + 0.1f * count / aots.Count));
            }
        }

        private async UniTask _enter_hotfix_main_scene()
        {
            // 等待用户输入
            // await _wait_for_enter_input();
            // 加载热更主场景
            SceneInstance scene = await Addressables.LoadSceneAsync(mainScene).Task;
            // 激活场景
            await scene.ActivateAsync();
        }

        // private async UniTask _wait_for_enter_input()
        // {
        //     switch (Application.platform)
        //     {
        //         case RuntimePlatform.WindowsPlayer:
        //             while (!Input.GetKey(KeyCode.Space))
        //             {
        //                 await UniTask.WaitForFixedUpdate();
        //             }
        //             break;
        //         case RuntimePlatform.Android:
        //             while (Input.touchCount == 0)
        //             {
        //                 await UniTask.WaitForFixedUpdate();
        //             }
        //             break;
        //     }
        // }
    }
}