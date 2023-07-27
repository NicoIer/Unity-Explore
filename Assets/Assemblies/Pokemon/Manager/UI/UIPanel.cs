using UnityEngine;

namespace Pokemon
{
    public abstract class UIPanel: MonoBehaviour
    {
        public virtual void OnCreate()
        {
            
        }

        public virtual void OnHide()
        {
            
        }

        public virtual void OnOpen()
        {
            
        }

        public virtual int Priority() => 0;

        public virtual UILayer Layer() => UILayer.Middle;
    }
}