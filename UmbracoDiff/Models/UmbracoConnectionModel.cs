using PropertyChanged;

namespace UmbracoDiff.Models
{
    [ImplementPropertyChanged]
    public class UmbracoConnectionModel
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public bool IsExpanded { get; set; }
    }
}