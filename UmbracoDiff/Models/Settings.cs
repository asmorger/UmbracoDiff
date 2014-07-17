using System.Collections.Generic;
using PropertyChanged;

namespace UmbracoDiff.Models
{
    [ImplementPropertyChanged]
    public class Settings
    {
        public IEnumerable<UmbracoConnection> Connections { get; set; }
    }
}