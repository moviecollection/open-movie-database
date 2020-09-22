using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    /// <summary>
    /// The <c>BaseModel</c> class.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether response was successful.
        /// </summary>
        [JsonProperty("Response")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets error text.
        /// </summary>
        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
