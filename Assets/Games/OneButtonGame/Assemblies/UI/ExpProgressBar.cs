using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nico;
using UnityEngine;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class ExpProgressBar : MonoBehaviour, IEventListener<ExpChange>
    {
        public Image fillImage;

        private void Awake()
        {
            EventManager.Listen<ExpChange>(this);
        }

        public void OnReceiveEvent(ExpChange e)
        {
            float targetPercent = e.currentExp / e.levelNeedExp;
            Debug.Log($"targetPercent:{targetPercent}");
            float delay = 0.5f;
            if (targetPercent > 1)
            {
                fillImage.DOFillAmount(1, delay);
                fillImage.DOFillAmount(targetPercent - 1, delay).SetDelay(delay);
            }
            else
            {
                fillImage.DOFillAmount(targetPercent, delay);
            }
        }
    }
}