using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pokemon
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxBackground : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 previousCameraPosition;
        public bool horizontal;
        public bool vertical;
        private SpriteRenderer spriteRenderer;
        private float textureUnitSizeX;
        private float textureUnitSizeY;
        public Vector2 multiplier = new Vector2(0.5f, 1f);
        private Transform _cameraTransform=>_mainCamera.transform;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;
            Texture texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            previousCameraPosition = _cameraTransform.position;
        }

        private void Update()
        {
            Vector3 deltaMovement = _mainCamera.transform.position - previousCameraPosition;
            
            if (horizontal)
            {
                transform.position += new Vector3(deltaMovement.x * multiplier.x, 0, 0);
                if (math.abs(_mainCamera.transform.position.x - transform.position.x) >= textureUnitSizeX)
                {
                    float offsetPositionX = (_mainCamera.transform.position.x - transform.position.x) % textureUnitSizeX;
                    transform.position =
                        new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
                }
            }
            


            if (vertical)
            {
                transform.position += new Vector3(0, deltaMovement.y * multiplier.y, 0);
                if (math.abs(_mainCamera.transform.position.y - transform.position.y) >= textureUnitSizeY)
                {
                    float offsetPositionY = (_mainCamera.transform.position.y - transform.position.y) % textureUnitSizeY;
                    transform.position =
                        new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY);
                }
            }
            


            previousCameraPosition = _mainCamera.transform.position;
        }
    }
}