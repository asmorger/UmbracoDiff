using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Events;
using UmbracoDiff.Models;
using UmbracoDiff.Services;
using UmbracoDiff.ViewModels.Settings;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class SettingsScreenViewModel : Screen, IScreenTab, IHandle<UmbracoConnectionRemovedEvent>
    {
        private readonly IComponentContext _componentContext;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

        public SettingsScreenViewModel(IComponentContext componentContext, ISettingsService settingsService, IEventAggregator eventAggregator)
        {
            _componentContext = componentContext;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);

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

            Connections.Clear();

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

        public void Handle(UmbracoConnectionRemovedEvent message)
        {
            var target = Connections.FirstOrDefault(c => string.Equals(c.Name, message.Name));

            if (target != null)
            {
                Connections.Remove(target);
            }
        }
    }
}