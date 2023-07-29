using System;
using OneButtonGame;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pokemon
{
    public class DebugSlider : MonoBehaviour, IPoolGameObject
    {
        public RectTransform rectTransform => transform as RectTransform;
        public string text
        {
            set => tmpText.text = value;
        }

        [SerializeField] private TextMeshProUGUI tmpText;

        [SerializeField] private Slider slider;

        public void AddListener(UnityAction<float> action)
        {
            slider.onValueChanged.AddListener(action);
        }

        PoolObjectState IPoolObject.state { get; set; }

        private void Awake()
        {
            slider.maxValue = 100;
            slider.minValue = 0;
        }

        public void OnSpawn()
        {
        }

        public void OnRecycle()
        {
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void SetValue(float value)
        {
            slider.SetValueWithoutNotify(value);
        }
    }
}