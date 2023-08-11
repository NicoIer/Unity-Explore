using System;

namespace ColliderTool
{
    public interface IColliderToolInput
    {
        public event Action<ColliderEditorTool> OnToolSelected;
    }

}