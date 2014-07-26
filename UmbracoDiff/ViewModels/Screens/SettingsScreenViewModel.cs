using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Models;
using UmbracoDiff.Services;
using UmbracoDiff.ViewModels.Dialogs;
using UmbracoDiff.ViewModels.Settings;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class SettingsScreenViewModel : Screen, IScreenTab
    {
        private readonly IComponentContext _componentContext;
        private readonly ISettingsService _settingsService;

        public IObservableCollection<UmbracoConnectionViewModel> Connections { get; set; }

        public UmbracoConnectionModel SelectedConnection { get; set; }

        public SettingsScreenViewModel(IComponentContext componentContext, ISettingsService settingsService)
        {
            _componentContext = componentContext;
            _settingsService = settingsService;

            Connections = new BindableCollection<UmbracoConnectionViewModel>();

            this.DisplayName = "Settings";
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

            Connections.Add(viewModel);
        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Settings;
        }
    }
}