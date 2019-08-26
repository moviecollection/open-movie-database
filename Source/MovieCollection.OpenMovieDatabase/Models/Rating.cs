using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Rating : object
    {
        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
