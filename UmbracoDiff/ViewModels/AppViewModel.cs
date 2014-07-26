using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Events;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff.ViewModels
{
    [ImplementPropertyChanged]
    public class AppViewModel : Conductor<IScreenTab>.Collection.OneActive, IHandle<NotConfiguredEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        public AppViewModel(IEnumerable<IScreenTab> tabs, IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            IOrderedEnumerable<IScreenTab> sortedTabs = tabs.OrderBy(t => (int) t.GetDisplayOrder());
            Items.AddRange(sortedTabs);

            _eventAggregator.Subscribe(this);
        }

        public void Handle(NotConfiguredEvent message)
        {
            ActivateTab<SettingsScreenViewModel>();

            /*
            var window = Application.Current.MainWindow as MetroWindow;

            if (window != null)
            {
                window.ShowMessageAsync("Settings Not Configured",
                    "Please configure the settings field and save them to use this application");
            }
            */
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "Umbraco Diff - Compare";
        }

        private void ActivateTab<TViewModel>() where TViewModel : IScreenTab
        {
            TViewModel tab = Items.OfType<TViewModel>().FirstOrDefault();

            if (tab != null)
            {
                ActivateItem(tab);
            }
        }
    }
}