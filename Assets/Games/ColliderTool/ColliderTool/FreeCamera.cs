using System;
using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// Editor模式下 SceneView的操作一致的相机 
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    internal class FreeCamera : SceneSingleton<FreeCamera>
    {
        public float speed = 10f;
        public float rotateSpeed = 720*6;
        public Camera Camera { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Camera = GetComponent<Camera>();
        }
        private void Update()
        {
            //按住鼠标右键时 WASD可以控制相机的移动
            if (Input.GetMouseButton(1))
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                transform.position += transform.right * (x * speed * Time.deltaTime);
                transform.position += transform.forward * (z * speed * Time.deltaTime);
                
                //鼠标的移动还可以控制相机的旋转
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                transform.Rotate(Vector3.up, mouseX * rotateSpeed * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.right, -mouseY * rotateSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                //鼠标滚轮控制相机的远近
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                transform.position += transform.forward * (scroll * speed);
            }
        }
    }
}