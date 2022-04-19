using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase;
using MovieCollection.OpenMovieDatabase.Models;

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
            _options = new OpenMovieDatabaseOptions("your-api-key-here");
            _service = new OpenMovieDatabaseService(_httpClient, _options);

            await InitializeMenu();
        }

        private static async Task InitializeMenu()
        {
Start:
            Console.Clear();
            Console.WriteLine("Welcome to the 'Open Movie Database' demo.\n");

            Console.WriteLine("1. Get Single Movie");
            Console.WriteLine("2. Get Movies");
            Console.WriteLine("3. Get Season");
            Console.WriteLine("4. Get Episode");

            Console.Write("\nPlease select an option: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            var task = input switch
            {
                1 => GetSingleMovieAsync(),
                2 => GetMoviesAsync(),
                3 => GetSeasonAsync(),
                4 => GetEpisodeAsync(),
                _ => null,
            };

            if (task != null)
            {
                await task;
            }

            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();

            goto Start;
        }

        private static async Task GetSingleMovieAsync()
        {
            var search = new NewMovieSearch
            {
                Query = "Iron Man",
            };

            var item = await _service.SearchMovieAsync(search.Query);

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

        private static async Task GetMoviesAsync()
        {
            var search = new NewMoviesSearch
            {
                Query = "three colors",
            };

            var items = await _service.SearchMoviesAsync(search);

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

        private static async Task GetSeasonAsync()
        {
            var season = await _service.SearchSeasonAsync("tt5687612", 1);

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

        private static async Task GetEpisodeAsync()
        {
            var episode = await _service.SearchEpisodeAsync("tt0262150", 2, 1);

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
