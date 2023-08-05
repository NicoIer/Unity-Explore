using System;

namespace Nico
{
    public interface IView
    {
        public TViewModel GetViewModel<TViewModel>() where TViewModel : class, IViewModel, new()
        {
            return ModelManager.Get<TViewModel>();
        }
        //和ViewModel进行绑定 当ViewModel发送通知使
        public void Bind<T>(Action<T> func) where T: IViewModel;
        public void UnBind<T>(Action<T> func) where T: IViewModel;
    }
}