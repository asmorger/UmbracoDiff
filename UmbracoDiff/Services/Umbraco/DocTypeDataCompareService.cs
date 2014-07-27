using System.Collections.Generic;
using System.Linq;
using UmbracoDiff.Entities;
using UmbracoDiff.Helpers;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services.Umbraco
{
    public interface IDocTypeDataCompareService : IDataCompareService<DocType, CmsNode>
    {
        
    }
    public class DocTypeDataCompareService : BaseUmbracoDataCompare<DocType>, IDocTypeDataCompareService
    {
        public override IList<DocType> GetData(string connectionString)
        {
            var nodeHelper = new CmsNodeHelper(connectionString);
            var result = nodeHelper.GetAllDocTypes().ToList();

            return result;
        }

        public new DataComparissonResult<CmsNode> GetResults(string leftConnectionString, string rightConnectionString)
        {
            var leftData = GetData(leftConnectionString);
            var rightData = GetData(rightConnectionString);

            var leftResult = CompareDataSets(leftData, rightData);
            var rightResult = CompareDataSets(rightData, leftData);

            var mismatchedProperties = CompareAndBindMismatchedDocTypes(leftData, rightData);

            var output = new MismatchedDataComparissonResult
            {
                LeftResult = leftResult,
                RightResult = rightResult,
                MismatchedResults = mismatchedProperties
            };

            return output;

            
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
