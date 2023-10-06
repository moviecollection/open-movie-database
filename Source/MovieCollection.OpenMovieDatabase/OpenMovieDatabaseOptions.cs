using System.Net.Http.Headers;

namespace MovieCollection.OpenMovieDatabase
{
    /// <summary>
    /// The <c>OpenMovieDatabaseOptions</c> class.
    /// </summary>
    public class OpenMovieDatabaseOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenMovieDatabaseOptions"/> class.
        /// </summary>
        public OpenMovieDatabaseOptions()
            : base()
        {
            ApiAddress = "https://www.omdbapi.com";
            ConvertNotAvailableToNull = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenMovieDatabaseOptions"/> class.
        /// </summary>
        /// <param name="apiKey">the api key.</param>
        public OpenMovieDatabaseOptions(string apiKey)
            : this()
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or sets base address to bypass restrictions if necessary.
        /// </summary>
        public string ApiAddress { get; set; }

        /// <summary>
        /// Gets or sets api key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether "N/A" values should be converted to null.
        /// </summary>
        /// <remarks>
        /// When a property value is not available, Open Movie Database server returns "N/A" as value.
        /// We defined a custom JsonConverter to convert any "N/A" to null.
        /// You can disable this behavior by setting this property to false.
        /// </remarks>
        public bool ConvertNotAvailableToNull { get; set; }

        /// <summary>
        /// Gets or sets the name (and version) of the product using this library.
        /// </summary>
        /// <remarks>
        /// This overrides the <see cref="System.Net.Http.HttpClient.DefaultRequestHeaders"/>.
        /// </remarks>
        public ProductHeaderValue ProductInformation { get; set; }
    }
}
