using Autofac;
using Caliburn.Micro;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff.ViewModels
{
    public class AppViewModel : Conductor<object>
    {
        private readonly IComponentContext _componentContext;
        private readonly IEventAggregator _eventAggregator;

        public AppViewModel(IComponentContext componentContext, IEventAggregator eventAggregator)
        {
            _componentContext = componentContext;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
        }

        protected override void OnInitialize()
        {
            Activate<CompareScreenViewModel>();
        }

        private void Activate<TViewModel>() where TViewModel : INotifyPropertyChangedEx
        {
            var viewModel = _componentContext.Resolve<TViewModel>();
            ActivateItem(viewModel);
        }
    }
}