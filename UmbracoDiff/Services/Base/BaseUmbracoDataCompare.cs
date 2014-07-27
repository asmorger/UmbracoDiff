using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UmbracoDiff.Entities;
using UmbracoDiff.Models;
using UmbracoDiff.ViewModels.CompareTabs;

namespace UmbracoDiff.Services
{
    public abstract class BaseUmbracoDataCompare<TDataType> : IDataCompareService<TDataType, CmsNodeViewModel> where TDataType : CmsNode, new()
    {
        public abstract IList<TDataType> GetData(string connectionString);

        public DataComparissonResult<CmsNodeViewModel> GetResults(string leftConnectionString, string rightConnectionString)
        {
            var leftData = GetData(leftConnectionString);
            var rightData = GetData(rightConnectionString);

            var leftResult = CompareDataSets(leftData, rightData);
            var rightResult = CompareDataSets(rightData, leftData);

            var mappedLeftResult = Mapper.Map<IEnumerable<CmsNodeViewModel>>(leftResult);
            var mappedRightResult = Mapper.Map<IEnumerable<CmsNodeViewModel>>(rightResult);

            var output = new DataComparissonResult<CmsNodeViewModel>
            {
                LeftResult = mappedLeftResult,
                RightResult = mappedRightResult
            };

            return output;
        }

        protected IEnumerable<CmsNode> CompareDataSets(IEnumerable<TDataType> left, IEnumerable<TDataType> right)
        {
            var same = from l in left
                       join r in right on l.Text.ToLower() equals r.Text.ToLower()
                       select l;

            var different = left.Except(same);
            return different;
        }
    }
}
