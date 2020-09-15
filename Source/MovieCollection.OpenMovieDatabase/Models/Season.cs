using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Season : BaseModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Season")]
        public string SeasonNumber { get; set; }

        [JsonProperty("totalSeasons")]
        public string TotalSeasons { get; set; }

        [JsonProperty("Episodes")]
        public IEnumerable<Episode> Episodes { get; set; }
    }
}
