using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nico;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class ExpProgressBar : MonoBehaviour, IEventListener<ExpChange>,IEventListener<LevelUp>
    {
        public Image bar;
        public TextMeshProUGUI title;
        private void OnEnable()
        {
            EventManager.Listen<ExpChange>(this);
            EventManager.Listen<LevelUp>(this);

        }

        private void OnDisable()
        {
            EventManager.UnListen<ExpChange>(this);
            EventManager.UnListen<LevelUp>(this);
        }

        private void Start()
        {
            bar.fillAmount = PlayerModelController.model.currentExp / PlayerModelController.GetLevelNeedExp();
            title.text = PlayerModelController.model.level.ToString();
        }

        public void OnReceiveEvent(ExpChange e)
        {
            float targetPercent = e.currentExp / e.levelNeedExp;
            // Debug.Log($"targetPercent:{targetPercent}");
            float delay = 0.5f;
            if (targetPercent > 1)
            {
                bar.DOFillAmount(1, delay);
                bar.DOFillAmount(targetPercent - 1, delay).SetDelay(delay);
            }
            else
            {
                bar.DOFillAmount(targetPercent, delay);
            }
        }

        public void OnReceiveEvent(LevelUp e)
        {
            title.DOText(e.level.ToString(), 0.5f);
        }
    }
}