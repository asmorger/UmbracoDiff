using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services
{
    internal class SettingsService : ISettingsService
    {
        private readonly string _jsonPath;

        private Settings settings;

        public SettingsService(string folderPath)
        {
            _jsonPath = Path.Combine(folderPath, "UmbracoDiff", "settings.json");

            var directoryName = Path.GetDirectoryName(_jsonPath);
            Directory.CreateDirectory(directoryName);
        }

        public bool IsConfigured
        {
            get { return File.Exists(_jsonPath); }
        }

        public Settings Get()
        {
            if (settings == null)
            {
                settings = Load();
            }
                
            return settings;
        }

        public void Save(Settings settings)
        {
            using (var sw = new StreamWriter(_jsonPath))
            {
                var serializeSettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
                sw.Write(serializeSettings);
            }
        }

        public Settings Load()
        {
            if (!File.Exists(_jsonPath))
            {
                settings = new Settings
                {
                    Connections = new Collection<UmbracoConnectionModel>()
                };

                return settings;
            }

            using (var sr = new StreamReader(_jsonPath))
            {
                var deserializeObject = JsonConvert.DeserializeObject<Settings>(sr.ReadToEnd().Trim());
                settings = deserializeObject;

                return deserializeObject;
            }
        }
    }
}