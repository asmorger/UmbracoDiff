using System.Collections.Generic;
using System.Linq;
using UmbracoDiff.Entities;
using UmbracoDiff.Helpers;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services.Umbraco
{
    public interface IDataTypeDataCompareService : IDataCompareService<DataType, CmsNode>
    {
        
    }
    public class DataTypesDataCompareService : BaseUmbracoDataCompare<DataType>, IDataTypeDataCompareService
    {
        public string LeftConnectionString { get; set; }
        public string RightConnectionString { get; set; }

        public override IList<DataType> GetData(string connectionString)
        {
            var nodeHelper = new CmsNodeHelper(connectionString);
            List<DataType> result = nodeHelper.GetAllDataTypes().ToList();

            return result;
        }
    }
}