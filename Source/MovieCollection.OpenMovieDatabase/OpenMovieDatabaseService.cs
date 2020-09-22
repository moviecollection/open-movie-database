using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase.Enums;
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
        public async Task<Movie> SearchMovieAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, PlotType plot = PlotType.Short)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["t"] = System.Web.HttpUtility.UrlEncode(query),
                ["plot"] = plot.ToString(),
            };

            if (type != SearchType.NotSpecified)
            {
                parameters.Add("type", type.ToString());
            }

            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add("y", year);
            }

            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);
        }

        /// <inheritdoc/>
        public async Task<Movie> SearchMovieByImdbIdAsync(string imdbid)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["i"] = System.Web.HttpUtility.UrlEncode(imdbid),
            };

            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);
        }

        /// <inheritdoc/>
        public async Task<Search> SearchMoviesAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, int page = 1)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["s"] = System.Web.HttpUtility.UrlEncode(query),
                ["page"] = page.ToString(CultureInfo.InvariantCulture),
            };

            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add("y", year);
            }

            if (type != SearchType.NotSpecified)
            {
                parameters.Add("type", type.ToString());
            }

            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Search>(json, _defaultJsonSettings);
        }

        /// <inheritdoc/>
        public async Task<Season> SearchSeasonAsync(string imdbid, int season)
        {
            var parameters = new Dictionary<string, string>()
            {
                ["i"] = System.Web.HttpUtility.UrlEncode(imdbid),
                ["season"] = season.ToString(CultureInfo.InvariantCulture),
            };

            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Season>(json, _defaultJsonSettings);
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

            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);
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
