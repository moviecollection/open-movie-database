using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class BaseModel : object
    {
        [JsonProperty("Response")]
        public bool Response { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
