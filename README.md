# Open Movie Database API
Unofficial implementation of Open Movie Database API for .NET

[![Nuget Version][nuget-shield]][nuget]
[![Nuget Downloads][nuget-shield-dl]][nuget]

## Installing
You can install this package by entering the following command into your `Package Manager Console`:
```powershell
Install-Package MovieCollection.OpenMovieDatabase
```

## How to use
First, define an instance of the `HttpClient` class if you haven't already.
```csharp
// HttpClient is intended to be instantiated once per application, rather than per-use.
// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
private static readonly HttpClient httpClient = new HttpClient();
```

Then, to search for a single movie:
```csharp
// using MovieCollection.OpenMovieDatabase;

var options = new OpenMovieDatabaseOptions("your-api-key-here");
var service = new OpenMovieDatabaseService(httpClient, options);

var movie = await service.SearchMovieAsync("interstellar");

if (!movie.IsSuccess)
{
    Console.WriteLine("Error: {0}", movie.Error);
}

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

## Search for movies
```csharp
// using MovieCollection.OpenMovieDatabase;

var options = new OpenMovieDatabaseOptions("your-api-key-here");
var service = new OpenMovieDatabaseService(httpClient, options);

var search = await service.SearchMoviesAsync("three colors");

if (!search.IsSuccess)
{
    Console.WriteLine("Error: {0}", search.Error);
}

foreach (var item in search.Items)
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

Please check out the `Demo` project for more examples.

## Null Conversion
When a value is not available the Open Movie Database server returns `"N/A"` instead of `null` which is inconvenient. By default we have a custom `JsonConverter` to convert them to `null`.

You can *disable* this feature by setting the `ConvertNotAvailableToNull` property to `false`.

```csharp
var options = new OpenMovieDatabaseOptions
{
    ConvertNotAvailableToNull = false,
};
```

## Notes
- Thanks to [Open Movie Database][omdb] for providing free API services. 
- Please read the Open Movie Database [terms of use][omdb-terms] before using their services.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.OpenMovieDatabase
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.OpenMovieDatabase.svg?label=Release
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.OpenMovieDatabase?label=Downloads&color=red

[omdb]: https://www.omdbapi.com
[omdb-terms]: https://www.omdbapi.com/legal.htm