namespace Nico
{
    public interface IViewModel : IModel, IView
    {
        public TView GetView<TView>() where TView : IView;
        public TModel GetModel<TModel>() where TModel : IModel;
    }
}