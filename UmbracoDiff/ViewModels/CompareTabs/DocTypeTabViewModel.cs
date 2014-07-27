using Caliburn.Micro;
using UmbracoDiff.Entities;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class DocTypeTabViewModel : BaseCompareTabViewModel<DocType, CmsNode>
    {
        public DocTypeTabViewModel(IDocTypeDataCompareService docTypeService, IEventAggregator eventAggregator) 
            : base(docTypeService, eventAggregator)
        {
            this.DisplayName = "doc types";
        }

        public override CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.DocType;
        }

        public void MismatchedItemsSelectionChanged()
        {
            
        }

        public void MismatchedDetailLoadingRow()
        {
            
        }

        public void MismatchedDetailSelectionChanged()
        {
            
        }
    }
}
