using UmbracoDiff.Enums;

namespace UmbracoDiff.ViewModels
{
    public interface ICompareTab
    {
        CompareTabDisplayOrder GetDisplayOrder();

        void Execute(string leftConnectionString, string rightConnectionString);
    }
}
