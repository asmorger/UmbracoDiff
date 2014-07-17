using PropertyChanged;

namespace UmbracoDiff.Models
{
    [ImplementPropertyChanged]
    public class UmbracoConnection
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}