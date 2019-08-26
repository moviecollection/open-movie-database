namespace MovieCollection.OpenMovieDatabase
{
    public class Configuration
    {
        public Configuration(string apiKey)
        {
            APIKey = apiKey;
        }

        /// <summary>
        /// Gets or sets API's base address to bypass restrictions if necessary.
        /// </summary>
        public string BaseAddress { get; set; } = "https://www.omdbapi.com";

        public string APIKey { get; set; }
    }
}
