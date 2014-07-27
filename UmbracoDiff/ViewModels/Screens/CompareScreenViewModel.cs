using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Events;
using UmbracoDiff.Models;
using UmbracoDiff.Services;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class CompareScreenViewModel : Conductor<ICompareTab>.Collection.OneActive, IScreenTab
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;

        public IObservableCollection<UmbracoConnectionModel> LeftConnections { get; set; }
        public IObservableCollection<UmbracoConnectionModel> RightConnections { get; set; }

        public UmbracoConnectionModel SelectedLeftConnection { get; set; }
        public UmbracoConnectionModel SelectedRightConnection { get; set; }

        public CompareScreenViewModel(IEventAggregator eventAggregator
                                    , ISettingsService settingsService
                                    , IEnumerable<ICompareTab> tabs)
        {
            _eventAggregator = eventAggregator;
            _settingsService = settingsService;

            LeftConnections = new BindableCollection<UmbracoConnectionModel>();
            RightConnections = new BindableCollection<UmbracoConnectionModel>();

            SelectedLeftConnection = new UmbracoConnectionModel();
            SelectedRightConnection = new UmbracoConnectionModel();

            var sortedTabs = tabs.OrderBy(t => (int) t.GetDisplayOrder());
            this.Items.AddRange(sortedTabs);

            this.DisplayName = "Compare";
        }

        protected override void OnActivate()
        {
            base.OnActivate();

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

        public void LeftItemChanged()
        {
            var settings = _settingsService.Get();
            settings.LeftConnection = SelectedLeftConnection;

            _settingsService.Save(settings);
        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Compare;
        }
    }
}