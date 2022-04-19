using MovieCollection.OpenMovieDatabase.Enums;

namespace MovieCollection.OpenMovieDatabase.Models
{
    public class NewMovieSearch
    {
        public NewMovieSearch()
        {
            SearchType = SearchType.NotSpecified;
            PlotType = PlotType.Default;
        }

        public string Query { get; set; }

        public string Year { get; set; }

        public SearchType SearchType { get; set; }

        public PlotType PlotType { get; set; }
    }
}
