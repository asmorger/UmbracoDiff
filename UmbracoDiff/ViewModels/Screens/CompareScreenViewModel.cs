using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Events;
using UmbracoDiff.Extensions;
using UmbracoDiff.Models;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class CompareScreenViewModel : Conductor<ICompareTab>.Collection.OneActive, IScreenTab, IHandle<DataLoadedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly IWindowManager _windowManager;

        public IObservableCollection<UmbracoConnectionModel> LeftConnections { get; set; }
        public IObservableCollection<UmbracoConnectionModel> RightConnections { get; set; }

        public UmbracoConnectionModel SelectedLeftConnection { get; set; }
        public UmbracoConnectionModel SelectedRightConnection { get; set; }

        public ProgressDialogController DialogController { get; set; }

        public List<DataLoadedEvent> DataLoadedEvents { get; set; }

        public CompareScreenViewModel(IEventAggregator eventAggregator
                                    , ISettingsService settingsService
                                    , IWindowManager windowManager
                                    , IEnumerable<ICompareTab> tabs)
        {
            _eventAggregator = eventAggregator;
            _settingsService = settingsService;
            _windowManager = windowManager;

            _eventAggregator.Subscribe(this);

            LeftConnections = new BindableCollection<UmbracoConnectionModel>();
            RightConnections = new BindableCollection<UmbracoConnectionModel>();

            SelectedLeftConnection = new UmbracoConnectionModel();
            SelectedRightConnection = new UmbracoConnectionModel();

            DataLoadedEvents = new List<DataLoadedEvent>();

            var sortedTabs = tabs.OrderBy(t => (int) t.GetDisplayOrder());
            this.Items.AddRange(sortedTabs);

            this.DisplayName = "Compare";
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            LeftConnections.Clear();
            RightConnections.Clear();

            if (!_settingsService.IsConfigured)
            {
                _eventAggregator.PublishOnUIThread(new NotConfiguredEvent());
                return;
            }

            var settings = _settingsService.Get();

            if (settings.Connections != null && settings.Connections.Any())
            {
                LeftConnections.AddRange(settings.Connections);
                RightConnections.AddRange(settings.Connections);
            }

            if (settings.LeftConnection != null)
            {
                SelectedLeftConnection = settings.LeftConnection;
            }

            if (settings.RightConnection != null)
            {
                SelectedLeftConnection = settings.RightConnection;
            }
        }

        public async void Compare()
        {
            DataLoadedEvents.Clear();

            var metroWinow = _windowManager.GetMetroWindow();
            DialogController = await metroWinow.ShowProgressAsync("Comparing", "Please wait");

            var tabsList = new List<ICompareTab>(Items);

            foreach (var tab in Items)
            {
                tab.Execute(SelectedLeftConnection.ConnectionString, SelectedRightConnection.ConnectionString);
            }
        }

        public void LeftItemChanged()
        {
            var settings = _settingsService.Get();
            settings.LeftConnection = SelectedLeftConnection;

            _settingsService.Save(settings);
        }

        public void RightItemChanged()
        {
            var settings = _settingsService.Get();
            settings.RightConnection = SelectedRightConnection;

            _settingsService.Save(settings);
        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Compare;
        }

        public void Handle(DataLoadedEvent message)
        {
            DataLoadedEvents.Add(message);

            if (DataLoadedEvents.Count == Items.Count)
            {
                DialogController.CloseAsync();
            }
        }
    }
}