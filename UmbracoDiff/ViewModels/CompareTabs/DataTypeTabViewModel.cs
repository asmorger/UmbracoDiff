using Caliburn.Micro;
using UmbracoDiff.Entities;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class DataTypeTabViewModel : BaseCompareTabViewModel<DataType, CmsNodeViewModel>, ICompareTab
    {
        public DataTypeTabViewModel(IDataTypeDataCompareService dataTypeService, IEventAggregator eventAggregator) 
            : base(dataTypeService, eventAggregator)
        {
            this.DisplayName = "data types";
        }

        public override CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.DataType;
        }
    }
}
