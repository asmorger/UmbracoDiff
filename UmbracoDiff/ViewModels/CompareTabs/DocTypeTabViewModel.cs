using Caliburn.Micro;
using PropertyChanged;
using UmbracoDiff.Entities;
using UmbracoDiff.Enums;
using UmbracoDiff.Models;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    [ImplementPropertyChanged]
    public class DocTypeTabViewModel : BaseCompareTabViewModel<DocType, CmsNode>
    {
        public IObservableCollection<MismatchedDocTypeItemModel> MismatchedResults { get; set; }

        public DocTypeTabViewModel(IDocTypeDataCompareService docTypeService, IEventAggregator eventAggregator) 
            : base(docTypeService, eventAggregator)
        {
            MismatchedResults = new BindableCollection<MismatchedDocTypeItemModel>();
            this.DisplayName = "doc types";
        }

        public override CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.DocType;
        }

        public override void PostExecute(DataComparissonResult<CmsNode> results)
        {
            var actualResult = results as MismatchedDataComparissonResult;

            if (actualResult != null)
            {
                MismatchedResults.Clear();
                MismatchedResults.AddRange(actualResult.MismatchedResults);
            }
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
