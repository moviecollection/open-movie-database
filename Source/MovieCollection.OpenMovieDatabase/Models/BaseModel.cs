using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    /// <summary>
    /// The <c>BaseModel</c> class.
    /// </summary>
    public class BaseModel : object
    {
        /// <summary>
        /// Gets or sets a value indicating whether response was successful.
        /// </summary>
        [JsonProperty("Response")]
        public bool Response { get; set; }

        /// <summary>
        /// Gets or sets error text.
        /// </summary>
        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
