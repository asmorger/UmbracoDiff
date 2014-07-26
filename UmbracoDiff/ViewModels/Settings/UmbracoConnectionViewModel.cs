using PropertyChanged;

namespace UmbracoDiff.ViewModels.Settings
{
    [ImplementPropertyChanged]
    public class UmbracoConnectionViewModel
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}