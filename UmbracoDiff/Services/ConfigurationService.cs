using System;
using System.IO;
using Newtonsoft.Json;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services
{
    internal class ConfigurationService : IConfigurationService
    {
        private readonly string _jsonPath;

        private Settings settings;

        public ConfigurationService()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _jsonPath = Path.Combine(appDataPath, "UmbracoDiff", "settings.json");

            var directoryName = Path.GetDirectoryName(_jsonPath);
            Directory.CreateDirectory(directoryName);
        }

        public bool IsConfigured
        {
            get { return File.Exists(_jsonPath); }
        }

        public Settings Settings
        {
            get
            {
                if (settings == null)
                    settings = Load();
                return settings;
            }
            set { settings = value; }
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
                Settings = new Settings();
                return Settings;
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