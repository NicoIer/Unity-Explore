using UnityEngine;

namespace Nico
{
    // 为什么用MonoBehavior？ 因为这样直观，通常UI代码不会对游戏性能造成影响，UI上挂MonoBehavior没有啥问题
    public abstract class UIPanel: MonoBehaviour
    {
        public virtual void OnCreate()
        {
            
        }

        // public virtual bool NeedDestroyWhenSceneChange()
        // {
        //     return true;
        // }

        public virtual void OnShow()
        {
            
        }

        public virtual void OnHide()
        {
            
        }

        public virtual int Priority() => 0;

        public virtual UILayer Layer() => UILayer.Middle;
    }
}