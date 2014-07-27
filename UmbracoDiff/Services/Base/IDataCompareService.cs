using System.Collections.Generic;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services
{
    public interface IDataCompareService<TEntity, TResultType> where TEntity : class where TResultType : class
    {
        IList<TEntity> GetData(string connectionString);

        DataComparissonResult<TResultType> GetResults(string leftConnectionString, string rightConnectionString);
    }
}
