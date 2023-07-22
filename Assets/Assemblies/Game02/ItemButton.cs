using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game03
{
    public class ItemButton: MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public TextMeshProUGUI nameText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one * 1.2f, 0.15f);
            nameText.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, 0.15f);
            nameText.enabled = false;
        }
    }
}