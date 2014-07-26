using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PropertyChanged;
using UmbracoDiff.Events;
using UmbracoDiff.Services;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff.ViewModels
{
    [ImplementPropertyChanged]
    public class AppViewModel : Conductor<IScreenTab>.Collection.OneActive, IHandle<NotConfiguredEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public AppViewModel(IEnumerable<IScreenTab> tabs, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
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

            var window = Application.Current.MainWindow as MetroWindow;

            if (window != null)
            {
                window.ShowMessageAsync("Settings Not Configured",
                    "Please configure the settings field and save them to use this application");
            }
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