using Nico;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pokemon
{
    public class DebugButton: MonoBehaviour,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.OpenUI<DebugPanel>();
            Debug.Log("DebugButton OnPointerClick");
        }
    }
}