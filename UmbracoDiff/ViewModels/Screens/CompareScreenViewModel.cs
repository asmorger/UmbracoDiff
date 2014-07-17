using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Models;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class CompareScreenViewModel : Screen
    {
        private readonly IConfigurationService _configurationService;
        private readonly IEventAggregator _eventAggregator;

        public UmbracoConnection LeftConnection { get; set; }
        public UmbracoConnection RightConnection { get; set; }

        public BindableCollection<UmbracoConnection> AllConnections { get; set; }

        public CompareScreenViewModel(IEventAggregator eventAggregator, IConfigurationService configurationService)
        {
            _eventAggregator = eventAggregator;
            _configurationService = configurationService;

            LeftConnection = new UmbracoConnection { Name = "Left" };
            RightConnection = new UmbracoConnection { Name = "Right" };

            AllConnections = new BindableCollection<UmbracoConnection>();
        }

        protected override void OnInitialize()
        {
            /*
            if (!_configurationService.IsConfigured)
            {
                _eventAggregator.PublishOnUIThread(new SettingsNotConfiguredEvent());
                return;
            }
            */

            // AllConnections.AddRange(_configurationService.Settings.Connections);
        }
    }
}