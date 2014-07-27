using Caliburn.Micro;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class DataTypeTabViewModel : Screen, ICompareTab
    {
        private readonly IDataTypeDataCompareService _dataTypeService;

        public DataTypeTabViewModel(IDataTypeDataCompareService dataTypeService)
        {
            _dataTypeService = dataTypeService;
        }

        public CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.DataType;
        }
    }
}
