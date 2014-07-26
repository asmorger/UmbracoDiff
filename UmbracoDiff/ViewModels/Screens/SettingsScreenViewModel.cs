using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls;
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
        private readonly IWindowManager _windowManager;
        private readonly ISettingsService _settingsService;

        public IObservableCollection<UmbracoConnectionViewModel> Connections { get; set; }

        public UmbracoConnectionModel SelectedConnection { get; set; }

        public SettingsScreenViewModel(IComponentContext componentContext, IWindowManager windowManager, ISettingsService settingsService)
        {
            _componentContext = componentContext;
            _windowManager = windowManager;
            _settingsService = settingsService;

            Connections = new BindableCollection<UmbracoConnectionViewModel>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "Settings";
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (_settingsService.IsConfigured)
            {
                var savedConnections = _settingsService.Get().Connections;
                var viewModels = Mapper.Map<IEnumerable<UmbracoConnectionViewModel>>(savedConnections);

                Connections.AddRange(viewModels);
            }
        }

        public void CreateConnection()
        {
            var dialog = _componentContext.Resolve<DbConnectionDialogViewModel>();
            _windowManager.ShowDialog(dialog);

            var window = Application.Current.MainWindow as MetroWindow;

        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Settings;
        }
    }
}