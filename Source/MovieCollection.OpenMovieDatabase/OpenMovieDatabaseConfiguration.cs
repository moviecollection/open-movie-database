namespace MovieCollection.OpenMovieDatabase
{
    public class OpenMovieDatabaseConfiguration
    {
        public OpenMovieDatabaseConfiguration()
            : base()
        {
            BaseAddress = "https://www.omdbapi.com";
            ConvertNotAvailableToNull = true;
        }

        public OpenMovieDatabaseConfiguration(string apiKey)
            : this()
        {
            APIKey = apiKey;
        }

        /// <summary>
        /// Gets or sets API's base address to bypass restrictions if necessary.
        /// </summary>
        public string BaseAddress { get; set; }

        public string APIKey { get; set; }

        /// <summary>
        /// When a property value is not available Open Movie Database server returns "N/A" as value.
        /// We defined a custom JsonConverter to convert any "N/A" to null.
        /// You can disable this behavior by setting this property to false.
        /// </summary>
        public bool ConvertNotAvailableToNull { get; set; }
    }
}
