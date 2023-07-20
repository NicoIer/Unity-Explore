using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entry
{
    public class EntryProgressBar : MonoBehaviour
    {
        public EntryManager entryManager;
        public Image progressBar;
        public TextMeshProUGUI progressText;

        private void Awake()
        {
            entryManager.progressChanged += OnProgressChanged;
        }

        private void OnProgressChanged(float obj)
        {
            progressBar.DOFillAmount(obj, 0.5f);
            progressText.DOText($"{Math.Round(obj * 100)}%", 0.5f, true, ScrambleMode.Numerals, "0123456789");
        }

        private void OnDestroy()
        {
            progressBar.DOKill();
            progressText.DOKill();
        }
    }
}