﻿using Autofac;
using Caliburn.Micro;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff.ViewModels
{
    public class AppViewModel : Conductor<IScreen>
    {
        private readonly IComponentContext _componentContext;
        private readonly IEventAggregator _eventAggregator;

        public AppViewModel(IComponentContext componentContext, IEventAggregator eventAggregator)
        {
            _componentContext = componentContext;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);

            this.DisplayName = "Umbraco Diff - Compare";
        }

        protected override void OnInitialize()
        {
            Activate<CompareScreenViewModel>();
        }

        private void Activate<TViewModel>() where TViewModel : IScreen
        {
            var viewModel = _componentContext.Resolve<TViewModel>();
            ActivateItem(viewModel);
        }
    }
}