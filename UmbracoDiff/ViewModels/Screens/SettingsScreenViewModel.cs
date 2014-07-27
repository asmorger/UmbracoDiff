using System.Collections.Generic;
using Autofac;
using AutoMapper;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Models;
using UmbracoDiff.Services;
using UmbracoDiff.ViewModels.Settings;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class SettingsScreenViewModel : Screen, IScreenTab
    {
        private readonly IComponentContext _componentContext;
        private readonly ISettingsService _settingsService;

        public SettingsScreenViewModel(IComponentContext componentContext, ISettingsService settingsService)
        {
            _componentContext = componentContext;
            _settingsService = settingsService;

            Connections = new BindableCollection<UmbracoConnectionViewModel>();

            DisplayName = "Settings";
        }

        public IObservableCollection<UmbracoConnectionViewModel> Connections { get; set; }

        public UmbracoConnectionModel SelectedConnection { get; set; }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Settings;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (_settingsService.IsConfigured)
            {
                var savedConnections = _settingsService.Get().Connections;
                var viewModels = Mapper.Map<ICollection<UmbracoConnectionViewModel>>(savedConnections);

                Connections.AddRange(viewModels);
            }
        }

        public void CreateConnection()
        {
            var viewModel = _componentContext.Resolve<UmbracoConnectionViewModel>();
            viewModel.Header = "New Connection";
            viewModel.IsExpanded = true;

            Connections.Add(viewModel);
        }
    }
}