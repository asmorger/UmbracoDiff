using System.Linq;
using Caliburn.Micro;
using UmbracoDiff.Enums;
using UmbracoDiff.Events;
using UmbracoDiff.Models;
using UmbracoDiff.Services;

namespace UmbracoDiff.ViewModels.CompareTabs
{
    public abstract class BaseCompareTabViewModel<TEntity, TResultType> : Screen, ICompareTab
        where TEntity : class where TResultType : class
    {
        protected readonly IEventAggregator _eventAggregator;
        protected readonly IDataCompareService<TEntity, TResultType> _compareService;

        public IObservableCollection<TResultType> LeftResults { get; set; }
        public IObservableCollection<TResultType> RightResults { get; set; }

        protected BaseCompareTabViewModel(IDataCompareService<TEntity, TResultType> compareService, IEventAggregator eventAggregator)
        {
            _compareService = compareService;
            _eventAggregator = eventAggregator;

            LeftResults = new BindableCollection<TResultType>();
            RightResults = new BindableCollection<TResultType>();
        }

        public abstract CompareTabDisplayOrder GetDisplayOrder();

        public virtual void PostExecute(DataComparissonResult<TResultType> results)
        {
            
        }

        public void Execute(string leftConnectionString, string rightConnectionString)
        {
            var results = _compareService.GetResults(leftConnectionString, rightConnectionString);

            if (results.LeftResult != null && results.LeftResult.Any())
            {
                LeftResults.Clear();
                LeftResults.AddRange(results.LeftResult);
            }

            if (results.RightResult != null && results.RightResult.Any())
            {
                RightResults.Clear();
                RightResults.AddRange(results.RightResult);
            }

            PostExecute(results);

            _eventAggregator.PublishOnBackgroundThread(new DataLoadedEvent());
        }
    }
}
