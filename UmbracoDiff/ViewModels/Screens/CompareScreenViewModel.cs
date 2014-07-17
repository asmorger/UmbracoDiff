using Caliburn.Micro;
using PropertyChanged;

namespace UmbracoDiff.ViewModels.Screens
{
    [ImplementPropertyChanged]
    public class CompareScreenViewModel : Screen
    {
        public string DisplayName { get; set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }
    }
}