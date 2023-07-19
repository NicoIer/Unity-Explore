using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace OneButtonGame
{
    public class EntryManager: MonoBehaviour
    {
        
        public async void Awake()
        {
            await _update_resources();
            await _load_aot_meta();
            await _load_hotupdate_dll();
            await _enter_game();
        }

        public async UniTask _update_resources()
        {
            //向指定的http地址请求资源列表
            //对比本地资源列表
            //下载资源
        }

        public async UniTask _load_aot_meta()
        {
            
        }

        public async UniTask _load_hotupdate_dll()
        {
            
        }

        public async UniTask _enter_game()
        {
            
        }
    }
}