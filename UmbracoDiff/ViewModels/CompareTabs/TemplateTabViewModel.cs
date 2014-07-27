using Caliburn.Micro;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class TemplateTabViewModel : Screen, ICompareTab
    {
        private readonly ITemplateDataCompareService _templateService;

        public TemplateTabViewModel(ITemplateDataCompareService templateService)
        {
            _templateService = templateService;
        }

        public CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.Template;
        }
    }
}
