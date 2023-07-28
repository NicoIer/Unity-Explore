using Pokemon;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace OneButtonGame
{
    public class BackToTitleButton : MonoBehaviour, IPointerClickHandler
    {
        public async void OnPointerClick(PointerEventData eventData)
        {
            await GlobalManager.Instance.sceneManager.ToTitle();
        }
    }
}