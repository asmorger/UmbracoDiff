using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Enums;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class SettingsScreenViewModel : Screen, IScreenTab
    {
        private readonly ISettingsService _settingsService;

        public SettingsScreenViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            DisplayName = "Settings";
        }

        public ScreenTabDisplayOrder GetDisplayOrder()
        {
            return ScreenTabDisplayOrder.Settings;
        }
    }
}