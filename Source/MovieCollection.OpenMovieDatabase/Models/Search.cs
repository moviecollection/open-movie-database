using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Search : BaseModel
    {
        [JsonProperty("Search")]
        public IEnumerable<SearchItem> Items { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }
    }
}
