using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GenshinImpact
{
    [Serializable,
     // 下面的特性用于在菜单中创建一个新的体积组件，后面的typeof是指定该体积组件可以在哪些渲染管线中使用 
     VolumeComponentMenuForRenderPipeline("Test/Test Bloom Effect",typeof(UniversalRenderPipeline))
    ]
    public class TestBloomEffectComponent :
        VolumeComponent, //来自 UnityEngine.Rendering
        IPostProcessComponent //来自 UnityEngine.Rendering.Universal
    {
        public bool IsActive()
        {
            return true;
        }

        public bool IsTileCompatible()
        {
            return false;
        }
    }
}