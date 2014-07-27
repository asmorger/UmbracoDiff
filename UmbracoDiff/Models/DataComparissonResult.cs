using System.Collections.Generic;

namespace UmbracoDiff.Models
{
    public class DataComparissonResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> LeftResult { get; set; }
        public IEnumerable<TEntity> RightResult { get; set; }
    }
}