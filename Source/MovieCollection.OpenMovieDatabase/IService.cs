﻿using MovieCollection.OpenMovieDatabase.Enums;
using MovieCollection.OpenMovieDatabase.Models;
using System.Threading.Tasks;

namespace MovieCollection.OpenMovieDatabase
{
    public interface IService
    {
        Task<Movie> SearchMovieByImdbIdAsync(string imdbId);
        Task<Movie> SearchMovieAsync(string query, string year = "", MovieTypes type = MovieTypes.NotSpecified, PlotTypes plot = PlotTypes.Short);
        Task<Search> SearchMoviesAsync(string query, string year = "", MovieTypes type = MovieTypes.NotSpecified, int page = 1);
        Task<Season> SearchSeasonAsync(string imdbId, int season);
        Task<Movie> SearchEpisodeAsync(string imdbId, int season, int episode);
    }
}