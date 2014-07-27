using System.Collections.Generic;
using System.Linq;
using UmbracoDiff.Entities;
using UmbracoDiff.Helpers;

namespace UmbracoDiff.Services.Umbraco
{
    public class DocTypeDataCompareService : BaseUmbracoDataCompare<DocType>
    {
        public override IList<DocType> GetData(string connectionString)
        {
            var nodeHelper = new CmsNodeHelper(connectionString);
            var result = nodeHelper.GetAllDocTypes().ToList();

            return result;
        }
    }
}
