using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace GenshinImpact
{
    public class TestPostProcessRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private Shader _bloomShader; //这个渲染特性的shader
        [SerializeField] private Shader _compatibleShader; //这个渲染特性的兼容shader
        private Material _bloomMaterial; //这个渲染特性的材质
        private Material _compositeMaterial; //
        private TestPostProcessPass _pass; //这个渲染特性的渲染pass

        //用于初始化
        public override void Create()
        {
            _bloomMaterial = CoreUtils.CreateEngineMaterial(_bloomShader);
            _compositeMaterial = CoreUtils.CreateEngineMaterial(_compatibleShader);
            _pass = new TestPostProcessPass(_bloomMaterial,_compositeMaterial);
        }
        //
        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                _pass.ConfigureInput(ScriptableRenderPassInput.Depth);
                _pass.ConfigureInput(ScriptableRenderPassInput.Color);
                _pass.ConfigureTarget(renderer.cameraColorTargetHandle,renderer.cameraDepthTargetHandle);
            }  
        }
        //
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }

        //记得销毁创建的材质
        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(_bloomMaterial);
            CoreUtils.Destroy(_compositeMaterial);
        }
    }
}