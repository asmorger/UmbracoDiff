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

        public bool IsDeleteVisible { get; set; }

        public UmbracoConnectionViewModel(ISettingsService settingsService, IEventAggregator eventAggregator)
        {
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ConnectionString))
            {
                return;
            }

            var model = Mapper.Map<UmbracoConnectionModel>(this);

            var settings = _settingsService.Get();

            RemoveCurrentItem(settings);

            settings.Connections.Add(model);
            _settingsService.Save(settings);

            this.Header = this.Name;
            this.IsExpanded = false;
        }

        public void Delete()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ConnectionString))
            {
                return;
            }

            var settings = _settingsService.Get();

            RemoveCurrentItem(settings);

            _settingsService.Save(settings);

            _eventAggregator.PublishOnUIThread(new UmbracoConnectionRemovedEvent(Name));
        }

        private bool RemoveCurrentItem(Models.Settings settings)
        {
            var itemRemoved = false;
            var existingModel = settings.Connections.FirstOrDefault(s => string.Equals(s.Name, this.Name));

            if (existingModel != null)
            {
                settings.Connections.Remove(existingModel);
                itemRemoved = true;
            }

            return itemRemoved;
        }
    }
}