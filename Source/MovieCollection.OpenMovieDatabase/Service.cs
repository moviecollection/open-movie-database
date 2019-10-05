using MovieCollection.OpenMovieDatabase.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.OpenMovieDatabase
{
    public class Service : IService
    {
        private readonly Configuration _configuration;
        private readonly JsonSerializerSettings _defaultJsonSettings;

        public Service(Configuration configuration)
            : base()
        {
            _configuration = configuration;
            _defaultJsonSettings = new JsonSerializerSettings();

            if (_configuration.ConvertNotAvailableToNull)
            {
                _defaultJsonSettings.Converters.Add(new Converters.NAStringConverter());
            }
        }

        private UrlParameter[] GetConfigParameters()
        {
            return new UrlParameter[]
            {
                new UrlParameter("apikey", _configuration.APIKey)
            };
        }

        private string GetParametersString(IEnumerable<UrlParameter> parameters)
        {
            StringBuilder builder = new StringBuilder();

            foreach (UrlParameter item in parameters)
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        private async Task<string> GetJsonAsync(IEnumerable<UrlParameter> parameters = null)
        {
            string url = _configuration.BaseAddress;

            var configParms = GetConfigParameters();

            if (parameters == null)
            {
                url += GetParametersString(configParms);
            }
            else
            {
                var union = parameters.Union(configParms);
                url += GetParametersString(union);
            }

            return await Helpers.DownloadJsonAsync(url);
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
            string json = await GetJsonAsync(parameters);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (result.Response == false)
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
            string json = await GetJsonAsync(parameters);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (result.Response == false)
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
            string json = await GetJsonAsync(parameters);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Search>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (result.Response == false)
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
                new UrlParameter("season", season.ToString())
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Season>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (result.Response == false)
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
                new UrlParameter("season", season.ToString()),
                new UrlParameter("episode", episode.ToString())
            };

            // Send Request And Get Json
            string json = await GetJsonAsync(parameters);

            // Deserialize
            var result = JsonConvert.DeserializeObject<Movie>(json, _defaultJsonSettings);

            // Throw an API Related Error 
            if (result.Response == false)
            {
                throw new Exception(result.Error);
            }

            return result;
        }
    }
}
