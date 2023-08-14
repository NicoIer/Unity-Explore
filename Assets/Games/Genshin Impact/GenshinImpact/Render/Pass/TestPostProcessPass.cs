using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GenshinImpact
{
    [Serializable]
    public class TestPostProcessPass: ScriptableRenderPass
    {
        private Material bloomMaterial;
        private Material compositeMaterial;
        private RenderTextureDescriptor _descriptor;
        private RTHandle cameraColorTarget;
        private RTHandle cameraDepthTarget;
        
        const int k_MaxPyramidBlurLevel = 16;
        private int[] _bloomMipUp;
        private int[] _bloomMipDown;
        private RTHandle[] _bloomMipUps;
        private RTHandle[] _bloomMipDowns;
        private GraphicsFormat hdrFormat;
        public TestPostProcessPass(Material bloomMaterial, Material compositeMaterial)
        {
            this.bloomMaterial = bloomMaterial;
            this.compositeMaterial = compositeMaterial;
            
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            
            _bloomMipUp = new int[k_MaxPyramidBlurLevel];
            _bloomMipDown = new int[k_MaxPyramidBlurLevel];
            _bloomMipUps = new RTHandle[k_MaxPyramidBlurLevel];
            _bloomMipDowns = new RTHandle[k_MaxPyramidBlurLevel];
            for (int i = 0; i < k_MaxPyramidBlurLevel; i++)
            {
                _bloomMipDown[i] = Shader.PropertyToID("_BloomMipDown" + i);
                _bloomMipUp[i] = Shader.PropertyToID("_BloomMipUp" + i);
                
                _bloomMipDowns[i] = RTHandles.Alloc(_bloomMipDown[i], name: "_BloomMipDown" + i);
                _bloomMipUps[i] = RTHandles.Alloc(_bloomMipUp[i], name: "_BloomMipUp" + i);
            }
            
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            using (CommandBuffer buffer = CommandBufferPool.Get())
            {
                
            }
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            _descriptor =  renderingData.cameraData.cameraTargetDescriptor;
        }
        
        public void SetTarget(RTHandle cameraColorTarget, RTHandle cameraDepthTarget)
        {
            this.cameraColorTarget = cameraColorTarget;
            this.cameraDepthTarget = cameraDepthTarget;
        }
        
    }
}