using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using PropertyChanged;
using UmbracoDiff.Events;
using UmbracoDiff.Extensions;
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

        public async void Handle(NotConfiguredEvent message)
        {
            ShowSettingsMessage();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "Umbraco Diff - Compare";
        }

        private async Task ShowSettingsMessage()
        {
            var window = _windowManager.GetMetroWindow();
            var result = await window.ShowMessageAsync("Settings Not Configured", "Please configure the settings field and save them to use this application");

            if (result == MessageDialogResult.Affirmative)
            {
                ActivateTab<SettingsScreenViewModel>();
            }
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