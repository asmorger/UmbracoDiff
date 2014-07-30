using PropertyChanged;
using UmbracoDiff.Entities;

namespace UmbracoDiff.Models
{
    [ImplementPropertyChanged]
    public class MismatchedDocTypeItemModel
    {
        public string Name { get { return Left.Text; } }
        public DocType Left { get; set; }
        public DocType Right { get; set; }
    }
}
