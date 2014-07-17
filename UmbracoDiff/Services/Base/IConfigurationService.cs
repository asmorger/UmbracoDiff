﻿using UmbracoDiff.Models;

namespace UmbracoDiff.Services
{
    public interface IConfigurationService
    {
        void Save(Settings settings);

        Settings Load();

        bool IsConfigured { get; }

        Settings Settings { get; set; }
    }
}
