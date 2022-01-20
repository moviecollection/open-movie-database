using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase.Enums;
using MovieCollection.OpenMovieDatabase.Models;

namespace MovieCollection.OpenMovieDatabase
{
    /// <summary>
    /// The <c>IOpenMovieDatabaseService</c> interface.
    /// </summary>
    [System.Obsolete("This interface is deprecated. Please use the 'OpenMovieDatabaseService' instead.")]
    public interface IOpenMovieDatabaseService
    {
        /// <summary>
        /// Searchs for a movie with imdb id.
        /// </summary>
        /// <param name="imdbId">The imdb id to search for.</param>
        /// <param name="plot">The plot type.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Movie> SearchMovieByImdbIdAsync(string imdbId, PlotType plot = PlotType.Default);

        /// <summary>
        /// Searchs for a movie.
        /// </summary>
        /// <param name="query">Movie title to search for.</param>
        /// <param name="year">Release year of the movie.</param>
        /// <param name="type">Type of the movie.</param>
        /// <param name="plot">Type of the plot.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Movie> SearchMovieAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, PlotType plot = PlotType.Default);

        /// <summary>
        /// Search for a list of movies.
        /// </summary>
        /// <param name="query">Movie title to search for.</param>
        /// <param name="year">Release year of the movie.</param>
        /// <param name="type">Type of the movie.</param>
        /// <param name="page">Results page number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Search> SearchMoviesAsync(string query, string year = "", SearchType type = SearchType.NotSpecified, int page = 1);

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
