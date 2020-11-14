using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase;

namespace Demo
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See: https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
        private static readonly HttpClient _httpClient = new HttpClient();

        private static OpenMovieDatabaseOptions _options;
        private static OpenMovieDatabaseService _service;

        private static async Task Main()
        {
            // Initialize
            _options = new OpenMovieDatabaseOptions("your-api-key-here");
            _service = new OpenMovieDatabaseService(_httpClient, _options);

            await GetSingleMovieAsync("interstellar");
            await GetMoviesAsync("three colors");
            await GetSeasonAsync("tt5687612", 1);
            await GetEpisodeAsync("tt0262150", 2, 1);

            // Wait for user to exit
            Console.ReadKey();
        }

        private static async Task GetSingleMovieAsync(string query)
        {
            Console.WriteLine($"-> Searching for '{query}'...\n");

            var item = await _service.SearchMovieAsync(query);

            if (!item.IsSuccess)
            {
                Console.WriteLine("Error: {0}", item.Error);
                return;
            }

            Console.WriteLine("Title: {0}", item.Title);
            Console.WriteLine("Year: {0}", item.Year);
            Console.WriteLine("Type: {0}", item.Type);
            Console.WriteLine("ImdbId: {0}", item.ImdbId);
            Console.WriteLine("ImdbRating: {0}", item.ImdbRating);
            Console.WriteLine("ImdbVotes: {0}", item.ImdbVotes);
            Console.WriteLine("Metascore: {0}", item.Metascore);
            Console.WriteLine("\n******************************\n");
        }

        private static async Task GetMoviesAsync(string query)
        {
            Console.WriteLine($"-> Searching for '{query}'...\n");

            var items = await _service.SearchMoviesAsync(query);

            if (!items.IsSuccess)
            {
                Console.WriteLine("Error: {0}", items.Error);
                return;
            }

            foreach (var item in items.Items)
            {
                Console.WriteLine("Title: {0}", item.Title);
                Console.WriteLine("Year: {0}", item.Year);
                Console.WriteLine("Type: {0}", item.Type);
                Console.WriteLine("ImdbId: {0}", item.ImdbId);
                Console.WriteLine("******************************");
            }
            
            Console.WriteLine("\n******************************\n");
        }

        private static async Task GetSeasonAsync(string imdbId, int seasonNumber)
        {
            Console.WriteLine($"-> Searching for '{imdbId}' - Season {seasonNumber:D2}\n");
            
            var season = await _service.SearchSeasonAsync(imdbId, seasonNumber);

            if (!season.IsSuccess)
            {
                Console.WriteLine("Error: {0}", season.Error);
                return;
            }

            Console.WriteLine("Title: {0}", season.Title);
            Console.WriteLine("SeasonNumber: {0}", season.SeasonNumber);
            Console.WriteLine("TotalSeasons: {0}", season.TotalSeasons);

            Console.WriteLine("Episodes:");

            foreach (var item in season.Episodes)
            {
                Console.WriteLine("******************************");
                Console.WriteLine("Title: {0}", item.Title);
                Console.WriteLine("Released: {0}", item.Released);
                Console.WriteLine("EpisodeNumber: {0}", item.EpisodeNumber);
                Console.WriteLine("ImdbId: {0}", item.ImdbId);
                Console.WriteLine("ImdbRating: {0}", item.ImdbRating);
            }

            Console.WriteLine("\n******************************\n");
        }

        private static async Task GetEpisodeAsync(string imdbId, int seasonNumber, int episodeNumber)
        {
            Console.WriteLine($"-> Searching for '{imdbId}' - Season {seasonNumber:D2} Episode {episodeNumber:D2}\n");
            
            var episode = await _service.SearchEpisodeAsync(imdbId, seasonNumber, episodeNumber);
            
            if (!episode.IsSuccess)
            {
                Console.WriteLine("Error: {0}", episode.Error);
                return;
            }

            Console.WriteLine("Title: {0}", episode.Title);
            Console.WriteLine("Year: {0}", episode.Year);
            Console.WriteLine("Type: {0}", episode.Type);
            Console.WriteLine("Season: {0}", episode.Season);
            Console.WriteLine("ImdbId: {0}", episode.ImdbId);
            Console.WriteLine("\n******************************\n");
        }
    }
}
