using System.Collections.Generic;

namespace BenivoNetwork.Common.Models
{
    public class SearchModel
    {
        public string Term { get; set; }
        public List<SearchResultModel> Results { get; set; }
    }
}
