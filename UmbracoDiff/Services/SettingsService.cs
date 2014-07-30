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
        private readonly IEncryptionService _encryption;

        private Settings settings;

        public SettingsService(string folderPath, IEncryptionService encryption)
        {
            _jsonPath = Path.Combine(folderPath, "UmbracoDiff", "settings.json");
            _encryption = encryption;

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
            using (var streamWriter = new StreamWriter(_jsonPath))
            {
                var serializeSettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
                var encryptedSource = _encryption.Encrypt(serializeSettings);
                
                streamWriter.Write(encryptedSource);
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

            using (var streamReader = new StreamReader(_jsonPath))
            {
                var encryptedSource = streamReader.ReadToEnd();
                var decryptedSource = _encryption.Decrypt(encryptedSource);

                var deserializeObject = JsonConvert.DeserializeObject<Settings>(decryptedSource.Trim());
                settings = deserializeObject;

                return deserializeObject;
            }
        }
    }
}