using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Events;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class CompareScreenViewModel : Screen, IScreenTab
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;

        public CompareScreenViewModel(IEventAggregator eventAggregator, ISettingsService settingsService)
        {
            _eventAggregator = eventAggregator;
            _settingsService = settingsService;

            this.DisplayName = "Compare";
        }

        protected override void OnInitialize()
        {
            if (!_settingsService.IsConfigured)
            {
                _eventAggregator.PublishOnUIThread(new NotConfiguredEvent());
                return;
            }
        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Compare;
        }
    }
}