using System.Collections.Generic;
using UmbracoDiff.Entities;

namespace UmbracoDiff.Models
{
    public class MismatchedDataComparissonResult : DataComparissonResult<CmsNode>
    {
        public IEnumerable<MismatchedDocTypeItemModel> MismatchedResults { get; set; }
    }
}