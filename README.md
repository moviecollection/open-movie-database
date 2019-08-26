[![Nuget Version](https://img.shields.io/nuget/v/MovieCollection.OpenMovieDatabase.svg?style=flat)](https://www.nuget.org/packages/MovieCollection.OpenMovieDatabase)
[![License](https://img.shields.io/github/license/peymanr34/open-movie-database.svg?style=flat)](LICENSE)

# Open Movie Database API
Minimal and unofficial implementation of Open Movie Database API

## Target frameworks
- .Net Standard 2.0
- .Net Framework 4.5.1

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json) > 12.0.2

## Installing
Enter following command in your ```NuGet Package Manager```:
```
Install-Package MovieCollection.OpenMovieDatabase -PreRelease
```

## How to search for a single movie

```csharp
// Initialize Configuration and OpenMovieDatabase Service
var configuration = new MovieCollection.OpenMovieDatabase.Configuration("your-api-key-here");
var service = new MovieCollection.OpenMovieDatabase.Service(configuration);

var movie = await service.SearchMovieAsync("interstellar");

Console.WriteLine("Title: {0}", movie.Title);
Console.WriteLine("Year: {0}", movie.Year);
Console.WriteLine("Type: {0}", movie.Type);
Console.WriteLine("ImdbId: {0}", movie.ImdbId);
Console.WriteLine("ImdbRating: {0}", movie.ImdbRating);
Console.WriteLine("ImdbVotes: {0}", movie.ImdbVotes);
Console.WriteLine("Metascore: {0}", movie.Metascore);
```
### Result:
```
Title: Interstellar
Year: 2014
Type: movie
ImdbId: tt0816692
ImdbRating: 8.6
ImdbVotes: 1,304,706
Metascore: 74
```

## How to search for movies

```csharp
// Initialize Configuration and OpenMovieDatabase Service
var configuration = new MovieCollection.OpenMovieDatabase.Configuration("your-api-key-here");
var service = new MovieCollection.OpenMovieDatabase.Service(configuration);

var movies = await service.SearchMoviesAsync("three colors");

foreach (var item in movies.Items)
{
    Console.WriteLine("Title: {0}", item.Title);
    Console.WriteLine("Year: {0}", item.Year);
    Console.WriteLine("Type: {0}", item.Type);
    Console.WriteLine("ImdbId: {0}", item.ImdbId);
    Console.WriteLine("******************************");
}
```
### Result:
```
Title: Three Colors: Red
Year: 1994
Type: movie
ImdbId: tt0111495
******************************
Title: Three Colors: Blue
Year: 1993
Type: movie
ImdbId: tt0108394
******************************
Title: Three Colors: White
Year: 1994
Type: movie
ImdbId: tt0111507
```

## Acknowledgments

Special thanks to [Open Movie Database](https://www.omdbapi.com) for providing free API services. 

## License
This project is licensed under the [MIT License](LICENSE).