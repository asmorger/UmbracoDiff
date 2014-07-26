using System.Collections.Generic;
using System.Linq;
using Autofac;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Events;
using UmbracoDiff.Services;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff.ViewModels
{
    [ImplementPropertyChanged]
    public class AppViewModel : Conductor<IScreenTab>.Collection.OneActive, IHandle<NotConfiguredEvent>
    {
        private readonly IComponentContext _componentContext;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;

        public AppViewModel(IEnumerable<IScreenTab> tabs
                          , IComponentContext componentContext
                          , IEventAggregator eventAggregator
                          , ISettingsService settingsService)
        {
            _componentContext = componentContext;
            _eventAggregator = eventAggregator;
            _settingsService = settingsService;

            var sortedTabs = tabs.OrderBy(t => (int) t.GetDisplayOrder());
            Items.AddRange(sortedTabs);

            _eventAggregator.Subscribe(this);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.DisplayName = "Umbraco Diff - Compare";
        }

        public void Handle(NotConfiguredEvent message)
        {
            ActivateTab<SettingsScreenViewModel>();
        }

        private void ActivateTab<TViewModel>() where TViewModel : IScreenTab
        {
            var tab = this.Items.OfType<TViewModel>().FirstOrDefault();

            if (tab != null)
            {
                this.ActivateItem(tab);
            }
        }
    }
}