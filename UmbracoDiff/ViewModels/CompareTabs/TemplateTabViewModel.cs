using Caliburn.Micro;
using UmbracoDiff.Enums;
using UmbracoDiff.Services.Umbraco;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public class TemplateTabViewModel : BaseCompareTabViewModel<string, string>
    {
        public TemplateTabViewModel(ITemplateDataCompareService templateService, IEventAggregator eventAggregator) 
            : base(templateService, eventAggregator)
        {
            this.DisplayName = "templates";
        }

        public override CompareTabDisplayOrder GetDisplayOrder()
        {
            return CompareTabDisplayOrder.Template;
        }
    }
}
