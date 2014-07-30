using System.Linq;
using AutoMapper;
using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Events;
using UmbracoDiff.Models;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.Settings
{
    [ImplementPropertyChanged]
    public class UmbracoConnectionViewModel
    {
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;

        public string Header { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public bool IsExpanded { get; set; }

        public UmbracoConnectionViewModel(ISettingsService settingsService, IEventAggregator eventAggregator)
        {
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
        }

        public void Save()
        {
            var model = Mapper.Map<UmbracoConnectionModel>(this);

            var settings = _settingsService.Get();

            var existingModel = settings.Connections.FirstOrDefault(s => string.Equals(s.Name, this.Name));

            if (existingModel != null)
            {
                settings.Connections.Remove(existingModel);
            }

            settings.Connections.Add(model);
            _settingsService.Save(settings);

            this.IsExpanded = false;
        }
    }
}