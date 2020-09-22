using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase.Models;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase
{
    /// <summary>
    /// The <c>OpenMovieDatabaseService</c> Class.
    /// </summary>
    public class OpenMovieDatabaseService : IOpenMovieDatabaseService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenMovieDatabaseOptions _options;
        private readonly JsonSerializerSettings _defaultJsonSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenMovieDatabaseService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <c>HttpClient</c>.</param>
        /// <param name="options">An instance of <see cref="OpenMovieDatabaseOptions"/>.</param>
        public OpenMovieDatabaseService(HttpClient httpClient, OpenMovieDatabaseOptions options)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _defaultJsonSettings = new JsonSerializerSettings();

            if (_options.ConvertNotAvailableToNull)
            {
                _defaultJsonSettings.Converters.Add(new Converters.NotAvailableStringConverter());
            }
        }

        /// <inheritdoc/>
        public async Task<Movie> SearchMovieAsync(string query, string year = "", Enums.MovieType type = Enums.MovieType.NotSpecified, Enums.PlotType plot = Enums.PlotType.Short)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["t"] = System.Web.HttpUtility.UrlEncode(query),
                ["plot"] = plot.ToString(),
            };

            // Movie Type [movie, series, episode]
            if (type != Enums.MovieType.NotSpecified)
            {
                parameters.Add("type", type.ToString());
            }

            // Year
            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add("y", year);
            }

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error.
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<Movie> SearchMovieByImdbIdAsync(string imdbid)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["i"] = System.Web.HttpUtility.UrlEncode(imdbid),
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error.
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<Search> SearchMoviesAsync(string query, string year = "", Enums.MovieType type = Enums.MovieType.NotSpecified, int page = 1)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["s"] = System.Web.HttpUtility.UrlEncode(query),
                ["page"] = page.ToString(CultureInfo.InvariantCulture),
            };

            // Year
            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add("y", year);
            }

            // Movie Type [movie, series, episode]
            if (type != Enums.MovieType.NotSpecified)
            {
                parameters.Add("type", type.ToString());
            }

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Search>(json, _defaultJsonSettings);

            // Throw an API Related Error.
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<Season> SearchSeasonAsync(string imdbid, int season)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["i"] = System.Web.HttpUtility.UrlEncode(imdbid),
                ["season"] = season.ToString(CultureInfo.InvariantCulture),
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Season>(json, _defaultJsonSettings);

            // Throw an API Related Error.
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<Movie> SearchEpisodeAsync(string imdbid, int season, int episode)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["i"] = System.Web.HttpUtility.UrlEncode(imdbid),
                ["season"] = season.ToString(CultureInfo.InvariantCulture),
                ["episode"] = episode.ToString(CultureInfo.InvariantCulture),
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error.
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        private static string GetParametersString(Dictionary<string, string> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in parameters)
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append($"{item.Key}={item.Value}");
            }

            return builder.ToString();
        }

        private async Task<string> GetJsonAsync(Dictionary<string, string> parameters)
        {
            string url = _options.ApiAddress;

            parameters.Add("apikey", _options.ApiKey);

            url += GetParametersString(parameters);

            using var response = await _httpClient.GetAsync(new Uri(url))
                .ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }
    }
}
