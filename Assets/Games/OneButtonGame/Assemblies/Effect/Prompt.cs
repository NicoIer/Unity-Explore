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
    
        private void Awake()
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        // private void OnDisable()
        // {
        //     transform.SetParent(null);
        // }

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            textMeshPro.color = Color.white;
        }

        public async void Print(string msg,Color color,float strength = 1.3f)
        {
            textMeshPro.color = color;
            textMeshPro.text = msg;
            textMeshPro.DOScale(strength, 0.5f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f)).ContinueWith(() =>
            {
                ObjectPoolManager.Return(gameObject);
            });
        }
    }
}