[![Nuget Version][nuget-ver-badge]][nuget]
[![Nuget Downloads][nuget-dl-badge]][nuget]

# Open Movie Database API
Unofficial implementation of Open Movie Database API.

## Target frameworks
- .NET Standard 2.0
- .NET Framework 4.5.1

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json) > 12.0.2

## Installing
Enter following command in your ```NuGet Package Manager```:
```powershell
Install-Package MovieCollection.OpenMovieDatabase -PreRelease
```

## Search for a single movie
1. Define an application wide `HttpClient` if you haven't already.
```csharp
// HttpClient is intended to be instantiated once per application, rather than per-use.
// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
private static readonly HttpClient httpClient = new HttpClient();
```

2. Initialize `OpenMovieDatabaseOptions` and `OpenMovieDatabaseService`:
```csharp
private async Task SearchSingleMovie()
{
    // Initialize
    var options = new OpenMovieDatabaseOptions("your-api-key-here");
    var service = new OpenMovieDatabaseService(httpClient, options);

    var movie = await service.SearchMovieAsync("interstellar");

    Console.WriteLine("Title: {0}", movie.Title);
    Console.WriteLine("Year: {0}", movie.Year);
    Console.WriteLine("Type: {0}", movie.Type);
    Console.WriteLine("ImdbId: {0}", movie.ImdbId);
    Console.WriteLine("ImdbRating: {0}", movie.ImdbRating);
    Console.WriteLine("ImdbVotes: {0}", movie.ImdbVotes);
    Console.WriteLine("Metascore: {0}", movie.Metascore);
}
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
private async Task SearchMoviesAsync()
{
    // Initialize
    var options = new OpenMovieDatabaseOptions("your-api-key-here");
    var service = new OpenMovieDatabaseService(httpClient, options);

    var movies = await service.SearchMoviesAsync("three colors");

    foreach (var item in movies.Items)
    {
        Console.WriteLine("Title: {0}", item.Title);
        Console.WriteLine("Year: {0}", item.Year);
        Console.WriteLine("Type: {0}", item.Type);
        Console.WriteLine("ImdbId: {0}", item.ImdbId);
        Console.WriteLine("******************************");
    }
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

## Convert N/A to null
When a property value is not available, Open Movie Database server returns `"N/A"` as value which is inconvenient. As of `v1.0.0-alpha.2` I defined a custom `JsonConverter` to convert every `"N/A"` to `null`. You can disable this behavior by setting `ConvertNotAvailableToNull = false` in `OpenMovieDatabaseOptions` object.

## Change log
Please visit releases page.

## Acknowledgments
Special thanks to [Open Movie Database](https://www.omdbapi.com) for providing free API services. 

## Open Movie Database API License
Please read Open Movie Database API license [here](https://www.omdbapi.com).

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.OpenMovieDatabase
[nuget-ver-badge]: https://img.shields.io/nuget/v/MovieCollection.OpenMovieDatabase.svg?style=flat
[nuget-dl-badge]: https://img.shields.io/nuget/dt/MovieCollection.OpenMovieDatabase?color=red