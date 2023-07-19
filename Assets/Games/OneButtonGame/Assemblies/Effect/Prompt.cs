using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nico;
using TMPro;
using UnityEngine;

namespace OneButtonGame
{
    public class Prompt : MonoBehaviour
    {
        public TMP_Text textMeshPro;
        public float timer;
        private void Awake()
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            textMeshPro.color = Color.white;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                ObjectPoolManager.Instance.Return(gameObject);
            }
        }

        public async void Print(string msg,Color color,float strength = 1.3f)
        {
            textMeshPro.color = color;
            textMeshPro.text = msg;
            textMeshPro.DOScale(strength, 0.5f);
        }
    }
}