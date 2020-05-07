﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase.Models;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase
{
    public class OpenMovieDatabaseService : IOpenMovieDatabaseService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenMovieDatabaseConfiguration _configuration;
        private readonly JsonSerializerSettings _defaultJsonSettings;

        public OpenMovieDatabaseService(HttpClient httpClient, OpenMovieDatabaseConfiguration configuration)
            : base()
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _defaultJsonSettings = new JsonSerializerSettings();

            if (_configuration.ConvertNotAvailableToNull)
            {
                _defaultJsonSettings.Converters.Add(new Converters.NAStringConverter());
            }
        }

        private static string GetParametersString(IEnumerable<UrlParameter> parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in parameters)
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        private async Task<string> GetJsonAsync(IEnumerable<UrlParameter> requestParameters = null)
        {
            string url = _configuration.BaseAddress;

            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("apikey", _configuration.APIKey)
            };

            if (requestParameters != null)
            {
                parameters.AddRange(requestParameters);
            }

            url += GetParametersString(parameters);

            using var response = await _httpClient.GetAsync(new Uri(url))
                .ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        public async Task<Movie> SearchMovieAsync(string query, string year = "", Enums.MovieType type = Enums.MovieType.NotSpecified, Enums.PlotType plot = Enums.PlotType.Short)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("t", System.Web.HttpUtility.UrlEncode(query)),
                new UrlParameter("plot", plot),
            };

            // Movie Type [movie, series, episode]
            if (type != Enums.MovieType.NotSpecified)
            {
                parameters.Add(new UrlParameter("type", type));
            }

            // Year
            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add(new UrlParameter("y", year));
            }

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        public async Task<Movie> SearchMovieByImdbIdAsync(string imdbid)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("i", System.Web.HttpUtility.UrlEncode(imdbid))
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        public async Task<Search> SearchMoviesAsync(string query, string year = "", Enums.MovieType type = Enums.MovieType.NotSpecified, int page = 1)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("s", System.Web.HttpUtility.UrlEncode(query)),
                new UrlParameter("page", page),
            };

            // Year
            if (!string.IsNullOrEmpty(year) && year.Length == 4)
            {
                parameters.Add(new UrlParameter("y", year));
            }

            // Movie Type [movie, series, episode]
            if (type != Enums.MovieType.NotSpecified)
            {
                parameters.Add(new UrlParameter("type", type));
            }

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Search>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        public async Task<Season> SearchSeasonAsync(string imdbid, int season)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("i", System.Web.HttpUtility.UrlEncode(imdbid)),
                new UrlParameter("season", season.ToString(CultureInfo.InvariantCulture))
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Season>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }

        public async Task<Movie> SearchEpisodeAsync(string imdbid, int season, int episode)
        {
            var parameters = new List<UrlParameter>()
            {
                new UrlParameter("i", System.Web.HttpUtility.UrlEncode(imdbid)),
                new UrlParameter("season", season.ToString(CultureInfo.InvariantCulture)),
                new UrlParameter("episode", episode.ToString(CultureInfo.InvariantCulture))
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters)
                .ConfigureAwait(false);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (!result.Response)
            {
                throw new Exception(result.Error);
            }

            return result;
        }
    }
}
