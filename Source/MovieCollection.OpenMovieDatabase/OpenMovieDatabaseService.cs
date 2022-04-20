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
    public class OpenMovieDatabaseService
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

            if (string.IsNullOrEmpty(_options.ApiAddress))
            {
                throw new ArgumentException($"'{nameof(_options.ApiAddress)}' cannot be null or empty.", nameof(options));
            }

            if (string.IsNullOrEmpty(_options.ApiKey))
            {
                throw new ArgumentException($"'{nameof(_options.ApiKey)}' cannot be null or empty.", nameof(options));
            }

            _defaultJsonSettings = new JsonSerializerSettings();

            if (_options.ConvertNotAvailableToNull)
            {
                _defaultJsonSettings.Converters.Add(new Converters.NotAvailableStringConverter());
            }
        }

        /// <summary>
        /// Searchs for a movie.
        /// </summary>
        /// <param name="query">The movie title to search for.</param>
        /// <param name="year">The release year of the movie.</param>
        /// <param name="type">The movie type.</param>
        /// <param name="plot">The plot type.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Movie> SearchMovieAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, PlotType plot = PlotType.Default)
        {
            var search = new NewMovieSearch
            {
                Query = query,
                Year = year,
                SearchType = type,
                PlotType = plot,
            };

            return SearchMovieAsync(search);
        }

        /// <summary>
        /// Searchs for a movie.
        /// </summary>
        /// <param name="search">An instance of the <see cref="NewMovieSearch"/> class.</param>
        /// <returns>A <see cref="Movie"/> object.</returns>
        public Task<Movie> SearchMovieAsync(NewMovieSearch search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            if (string.IsNullOrEmpty(search.Query))
            {
                throw new ArgumentException($"'{search.Query}' cannot be null or empty.", nameof(search));
            }

            var parameters = new Dictionary<string, string>()
            {
                ["t"] = search.Query,
            };

            if (search.PlotType == PlotType.Brief)
            {
                parameters.Add("plot", "short");
            }
            else if (search.PlotType == PlotType.Full)
            {
                parameters.Add("plot", "full");
            }

            if (search.SearchType != SearchType.NotSpecified)
            {
                parameters.Add("type", search.SearchType.ToString());
            }

            // TODO: Consider throwing an exception on invalid years.
            if (!string.IsNullOrEmpty(search.Year) && search.Year.Length == 4)
            {
                parameters.Add("y", search.Year);
            }

            return GetJsonAsync<Movie>(parameters);
        }

        /// <summary>
        /// Searchs for a movie with imdb id.
        /// </summary>
        /// <param name="imdbid">The imdb id to search for.</param>
        /// <param name="plot">The plot type.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Movie> SearchMovieByImdbIdAsync(string imdbid, PlotType plot = PlotType.Default)
        {
            if (string.IsNullOrEmpty(imdbid))
            {
                throw new ArgumentException($"'{nameof(imdbid)}' cannot be null or empty.", nameof(imdbid));
            }

            var parameters = new Dictionary<string, string>()
            {
                ["i"] = imdbid,
            };

            if (plot == PlotType.Brief)
            {
                parameters.Add("plot", "short");
            }
            else if (plot == PlotType.Full)
            {
                parameters.Add("plot", "full");
            }

            return GetJsonAsync<Movie>(parameters);
        }

        /// <summary>
        /// Search for a list of movies.
        /// </summary>
        /// <param name="query">The movie title to search for.</param>
        /// <param name="year">The release year of the movie.</param>
        /// <param name="type">The movie type.</param>
        /// <param name="page">The results page number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Search> SearchMoviesAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, int page = 1)
        {
            var search = new NewMoviesSearch
            {
                Query = query,
                Year = year,
                SearchType = type,
                Page = page,
            };

            return SearchMoviesAsync(search);
        }

        /// <summary>
        /// Search for a list of movies.
        /// </summary>
        /// <param name="search">An instance of the <see cref="NewMoviesSearch"/> class.</param>
        /// <returns>A <see cref="Search"/> object.</returns>
        public Task<Search> SearchMoviesAsync(NewMoviesSearch search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            if (string.IsNullOrEmpty(search.Query))
            {
                throw new ArgumentException($"'{nameof(search.Query)}' cannot be null or empty.", nameof(search));
            }

            var parameters = new Dictionary<string, string>()
            {
                ["s"] = search.Query,
            };

            // TODO: Consider throwing an exception on invalid years.
            if (!string.IsNullOrEmpty(search.Year) && search.Year.Length == 4)
            {
                parameters.Add("y", search.Year);
            }

            if (search.SearchType != SearchType.NotSpecified)
            {
                parameters.Add("type", search.SearchType.ToString());
            }

            if (search.Page.HasValue)
            {
                parameters.Add("page", search.Page.Value.ToString(CultureInfo.InvariantCulture));
            }

            return GetJsonAsync<Search>(parameters);
        }

        /// <summary>
        /// Gets season information of a series.
        /// </summary>
        /// <param name="imdbid">ImdbId of the series.</param>
        /// <param name="season">Season number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Season> SearchSeasonAsync(string imdbid, int season)
        {
            if (string.IsNullOrEmpty(imdbid))
            {
                throw new ArgumentException($"'{nameof(imdbid)}' cannot be null or empty.", nameof(imdbid));
            }

            var parameters = new Dictionary<string, string>()
            {
                ["i"] = imdbid,
                ["season"] = season.ToString(CultureInfo.InvariantCulture),
            };

            return GetJsonAsync<Season>(parameters);
        }

        /// <summary>
        /// Gets episode information of a series.
        /// </summary>
        /// <param name="imdbid">The imdb id of the series.</param>
        /// <param name="season">The season number.</param>
        /// <param name="episode">The episode number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Movie> SearchEpisodeAsync(string imdbid, int season, int episode)
        {
            if (string.IsNullOrEmpty(imdbid))
            {
                throw new ArgumentException($"'{nameof(imdbid)}' cannot be null or empty.", nameof(imdbid));
            }

            var parameters = new Dictionary<string, string>()
            {
                ["i"] = imdbid,
                ["season"] = season.ToString(CultureInfo.InvariantCulture),
                ["episode"] = episode.ToString(CultureInfo.InvariantCulture),
            };

            return GetJsonAsync<Movie>(parameters);
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

        private async Task<T> GetJsonAsync<T>(Dictionary<string, string> parameters)
            where T : BaseModel
        {
            string url = _options.ApiAddress;

            parameters.Add("apikey", _options.ApiKey);

            url += GetParametersString(parameters);

            using var response = await _httpClient.GetAsync(new Uri(url))
                .ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json, _defaultJsonSettings);
        }
    }
}
