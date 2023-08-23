namespace ColliderTool
{


    public abstract class ColliderEditorTool
    {


        protected DrawPlane plane => Driver.Instance.currentPlane;

        internal virtual void OnUpdate()
        {
        }
        
        public virtual void OnDisable()
        {
        }

        public virtual void OnEnable()
        {
        }
    }
}