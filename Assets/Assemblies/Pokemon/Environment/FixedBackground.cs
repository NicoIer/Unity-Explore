using System;
using UnityEngine;

namespace Pokemon
{
    public class FixedBackground: MonoBehaviour
    {
        private Camera _mainCamera;
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0);
        }
    }
}