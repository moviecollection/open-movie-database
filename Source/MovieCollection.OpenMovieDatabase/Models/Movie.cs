using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class Movie : BaseModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Rated")]
        public string Rated { get; set; }

        [JsonProperty("Released")]
        public string Released { get; set; }

        [JsonProperty("Runtime")]
        public string Runtime { get; set; }

        [JsonProperty("Genre")]
        public string Genre { get; set; }

        [JsonProperty("Director")]
        public string Director { get; set; }

        [JsonProperty("Writer")]
        public string Writer { get; set; }

        [JsonProperty("Actors")]
        public string Actors { get; set; }

        [JsonProperty("Plot")]
        public string Plot { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Awards")]
        public string Awards { get; set; }

        [JsonProperty("Poster")]
        public string Poster { get; set; }

        [JsonProperty("Ratings")]
        public IEnumerable<Rating> Ratings { get; set; }

        [JsonProperty("Metascore")]
        public string Metascore { get; set; }

        [JsonProperty("imdbRating")]
        public string ImdbRating { get; set; }

        [JsonProperty("imdbVotes")]
        public string ImdbVotes { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("DVD")]
        public string DVD { get; set; }

        /// <summary>
        /// Gets or sets movie boxOffice.
        /// </summary>
        /// <remarks>
        /// This value is only available for movies.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("BoxOffice")]
        public string BoxOffice { get; set; }

        /// <summary>
        /// Gets or sets movie production.
        /// </summary>
        /// <remarks>
        /// This value is only available for movies.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("Production")]
        public string Production { get; set; }

        /// <summary>
        /// Gets or sets movie website.
        /// </summary>
        /// <remarks>
        /// This value is only available for movies.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("Website")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets season number.
        /// </summary>
        /// <remarks>
        /// This value is only available for episodes.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("Season")]
        public string Season { get; set; }

        /// <summary>
        /// Gets or sets episode number.
        /// </summary>
        /// <remarks>
        /// This value is only available for episodes.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("Episode")]
        public string Episode { get; set; }

        /// <summary>
        /// Gets or sets id of the series.
        /// </summary>
        /// <remarks>
        /// This value is only available for episodes.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("seriesID")]
        public string SeriesId { get; set; }

        /// <summary>
        /// Gets or sets total season count of the series.
        /// </summary>
        /// <remarks>
        /// This value is only available for series.
        /// Please check <see cref="Type"/> for availability.
        /// </remarks>
        [JsonProperty("totalSeasons")]
        public string TotalSeasons { get; set; }
    }
}
