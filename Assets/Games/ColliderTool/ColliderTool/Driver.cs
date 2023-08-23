using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace ColliderTool
{
    [RequireComponent(typeof(GridSystemRender))]
    [RequireComponent(typeof(FreeCamera))]
    [RequireComponent(typeof(IColliderToolInput))]
    internal class Driver : SceneSingleton<Driver>
    {
        private IColliderToolInput _input;
        private FreeCamera _freeCamera;
        private ColliderEditorTool _currentTool;
        public Vector3 intersection { get; private set; }
        public Grid currentGrid => new Grid(intersection, gridSize);
        private Surface Surface => new Surface(Vector3.up, -DrawPlane.Instance.transform.position.y);
        private GridSystemRender _render;
        public Vector3 gridSize = Vector3.one;
        public DrawPlane currentPlane => DrawPlane.Instance;


        protected override void Awake()
        {
            base.Awake();
            _input = GetComponent<IColliderToolInput>();
            _freeCamera = GetComponent<FreeCamera>();
            _render = GetComponent<GridSystemRender>();
        }

        private void OnEnable()
        {
            _input.OnToolSelected += OnToolSelected;
        }

        private void OnDisable()
        {
            _input.OnToolSelected -= OnToolSelected;
        }

        internal void OnToolSelected(ColliderEditorTool tool)
        {
            _currentTool?.OnDisable();
            _currentTool = tool;
            _currentTool?.OnEnable();
        }

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _freeCamera.Camera.ScreenPointToRay(mousePos);
            intersection = SpaceGeometry.RaySurfaceIntersection(ray, Surface);
            if (_currentTool == null) return;

            _currentTool.OnUpdate();
        }
    }
}