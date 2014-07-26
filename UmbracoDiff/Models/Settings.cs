using System.Collections.Generic;
using PropertyChanged;

namespace UmbracoDiff.Models
{
    [ImplementPropertyChanged]
    public class Settings
    {
        public ICollection<UmbracoConnectionModel> Connections { get; set; }

        public UmbracoConnectionModel LeftConnection { get; set; }
        public UmbracoConnectionModel RightConnection { get; set; }
    }
}