using System.Collections.Generic;
using System.Linq;
using UmbracoDiff.Entities;
using UmbracoDiff.Helpers;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services.Umbraco
{
    public class DataTypesDataCompareService : BaseUmbracoDataCompare<DataType>
    {
        public string LeftConnectionString { get; set; }
        public string RightConnectionString { get; set; }

        public override IList<DataType> GetData(string connectionString)
        {
            var nodeHelper = new CmsNodeHelper(connectionString);
            List<DataType> result = nodeHelper.GetAllDataTypes().ToList();

            return result;
        }

        
        public new DataComparissonResult<CmsNode> GetResults(string leftConnectionString, string rightConnectionString)
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
        

        private IEnumerable<CmsNode> CompareDataSets(IEnumerable<CmsNode> left, IEnumerable<CmsNode> right)
        {
            var same = from l in left
                join r in right on l.Text.ToLower() equals r.Text.ToLower()
                select l;

            var different = left.Except(same);
            return different;
        }

        private IList<MismatchedDocTypeItemModel> CompareAndBindMismatchedDocTypes(IEnumerable<DocType> left, IEnumerable<DocType> right)
        {
            var mismatchedProperties = new List<MismatchedDocTypeItemModel>();

            var rightDocTypes = right.ToList();
            foreach (var docTypeLeft in left)
            {
                var docTypeRight = rightDocTypes.FirstOrDefault(x => x.Text == docTypeLeft.Text);
                if (docTypeRight != null)
                {
                    if (!docTypeLeft.PropertiesAreEqual(docTypeRight))
                    {
                        mismatchedProperties.Add(new MismatchedDocTypeItemModel { Left = docTypeLeft, Right = docTypeRight });
                    }
                }
            }

            return mismatchedProperties;
        }
    }
}