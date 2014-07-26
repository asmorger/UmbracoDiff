using AutoMapper;
using PropertyChanged;
using UmbracoDiff.Models;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Settings
{
    [ImplementPropertyChanged]
    public class UmbracoConnectionViewModel
    {
        private readonly ISettingsService _settingsService;

        public string Header { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public bool IsExpanded { get; set; }

        public UmbracoConnectionViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void Save()
        {
            var model = Mapper.Map<UmbracoConnectionModel>(this);

            var settings = _settingsService.Get();
            settings.Connections.Add(model);

            _settingsService.Save(settings);
        }
    }
}