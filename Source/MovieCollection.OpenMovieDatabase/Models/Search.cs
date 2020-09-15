using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Search : object
    {
        [JsonProperty("Search")]
        public IEnumerable<SearchItem> Items { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("Response")]
        public bool Response { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
