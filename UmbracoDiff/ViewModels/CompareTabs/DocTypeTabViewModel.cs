using Caliburn.Micro;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class DocTypeTabViewModel : Screen, ICompareTab
    {
        private readonly IDocTypeDataCompareService _docTypeService;

        public DocTypeTabViewModel(IDocTypeDataCompareService docTypeService)
        {
            _docTypeService = docTypeService;
        }

        public CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.DocType;
        }
    }
}
