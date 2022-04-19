using MovieCollection.OpenMovieDatabase.Enums;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class NewMoviesSearch
    {
        public NewMoviesSearch()
        {
            SearchType = SearchType.NotSpecified;
        }

        public string Query { get; set; }

        public string Year { get; set; }

        public SearchType SearchType { get; set; }

        public int? Page { get; set; }
    }
}
