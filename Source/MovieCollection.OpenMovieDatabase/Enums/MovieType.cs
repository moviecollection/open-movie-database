namespace MovieCollection.OpenMovieDatabase.Enums
{
    /// <summary>
    /// Specifies movie type for search.
    /// </summary>
    public enum MovieType : int
    {
        /// <summary>
        /// Movie type can be a movie or a series.
        /// </summary>
        NotSpecified = -1,

        /// <summary>
        /// Only return movies.
        /// </summary>
        Movie = 0,

        /// <summary>
        /// Only return series.
        /// </summary>
        Series = 1,
    }
}
