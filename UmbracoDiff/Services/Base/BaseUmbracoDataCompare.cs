using System.Collections.Generic;
using System.Linq;
using UmbracoDiff.Entities;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services
{
    public abstract class BaseUmbracoDataCompare<TDataType> : IDataCompareService<TDataType, CmsNode> where TDataType : CmsNode, new()
    {
        public abstract IList<TDataType> GetData(string connectionString);

        public DataComparissonResult<CmsNode> GetResults(string leftConnectionString, string rightConnectionString)
        {
            var leftData = GetData(leftConnectionString);
            var rightData = GetData(rightConnectionString);

            var leftResult = CompareDataSets(leftData, rightData);
            var rightResult = CompareDataSets(rightData, leftData);

            var output = new DataComparissonResult<CmsNode>
            {
                LeftResult = leftResult,
                RightResult = rightResult
            };

            return output;
        }

        private IEnumerable<CmsNode> CompareDataSets(IEnumerable<TDataType> left, IEnumerable<TDataType> right)
        {
            var same = from l in left
                       join r in right on l.Text.ToLower() equals r.Text.ToLower()
                       select l;

            var different = left.Except(same);
            return different;
        }
    }
}
