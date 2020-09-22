using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Episode
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Released")]
        public string Released { get; set; }

        [JsonProperty("Episode")]
        public string EpisodeNumber { get; set; }

        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }
    }
}
