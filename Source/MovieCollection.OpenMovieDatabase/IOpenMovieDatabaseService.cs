using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase.Enums;
using MovieCollection.OpenMovieDatabase.Models;

namespace MovieCollection.OpenMovieDatabase
{
    /// <summary>
    /// The <c>IOpenMovieDatabaseService</c> interface.
    /// </summary>
    public interface IOpenMovieDatabaseService
    {
        /// <summary>
        /// Searchs for a movie with imdbId.
        /// </summary>
        /// <param name="imdbId">The imdbId to search for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Movie> SearchMovieByImdbIdAsync(string imdbId);

        /// <summary>
        /// Searchs for a movie.
        /// </summary>
        /// <param name="query">Movie title to search for.</param>
        /// <param name="year">Release year of the movie.</param>
        /// <param name="type">Type of the movie.</param>
        /// <param name="plot">Type of the plot.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Movie> SearchMovieAsync(string query, string year = "", MovieType type = MovieType.NotSpecified, PlotType plot = PlotType.Short);

        /// <summary>
        /// Search for a list of movies.
        /// </summary>
        /// <param name="query">Movie title to search for.</param>
        /// <param name="year">Release year of the movie.</param>
        /// <param name="type">Type of the movie.</param>
        /// <param name="page">Results page number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Search> SearchMoviesAsync(string query, string year = "", MovieType type = MovieType.NotSpecified, int page = 1);

        /// <summary>
        /// Gets season information of a series.
        /// </summary>
        /// <param name="imdbId">ImdbId of the series.</param>
        /// <param name="season">Season number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Season> SearchSeasonAsync(string imdbId, int season);

        /// <summary>
        /// Gets episode information of a series.
        /// </summary>
        /// <param name="imdbId">ImdbId of the series.</param>
        /// <param name="season">Season number.</param>
        /// <param name="episode">Episode number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Movie> SearchEpisodeAsync(string imdbId, int season, int episode);
    }
}
