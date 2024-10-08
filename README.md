# Open Movie Database API
Unofficial implementation of the Open Movie Database API for .NET

[![NuGet Version][nuget-shield]][nuget]
[![NuGet Downloads][nuget-shield-dl]][nuget]

## Installation
You can install this package via the `Package Manager Console` in Visual Studio.

```powershell
Install-Package MovieCollection.OpenMovieDatabase
```

## Configuration
Get or create a new static `HttpClient` instance if you don't have one already.

```csharp
// HttpClient lifecycle management best practices:
// https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
private static readonly HttpClient httpClient = new HttpClient();
```

Then, you need to set your api key and pass it to the service's constructor.

```csharp
// using MovieCollection.OpenMovieDatabase;

var options = new OpenMovieDatabaseOptions
{
    ApiKey = "your-api-key",
};

var service = new OpenMovieDatabaseService(httpClient, options);
```

## Search for a Movie
You can search for a movie via the `SearchMovieAsync` method.

```csharp
var search = new NewMovieSearch
{
    Query = "interstellar",
};

var item = await service.SearchMovieAsync(search);
```

## Search for Movies
You can search for movies via the `SearchMoviesAsync` method.

```csharp
var search = new NewMoviesSearch
{
    Query = "three colors",
};

var result = await service.SearchMoviesAsync(search);
```

## Search for a Season
You can search for a season via the `SearchSeasonAsync` method.

```csharp
var season = await service.SearchSeasonAsync("tt2788316", 1);
```

## Search for an Episode
You can search for an episode via the `SearchEpisodeAsync` method.

```csharp
var episode = await service.SearchEpisodeAsync("tt2788316", 1, 2);
```

Please see the demo project for more examples.

## Null Conversion
When a value is not available the Open Movie Database server returns `"N/A"` instead of `null` which is inconvenient. By default we have a custom `JsonConverter` to convert them to `null`.

You can *disable* this feature by setting the `ConvertNotAvailableToNull` option to `false`.

```csharp
var options = new OpenMovieDatabaseOptions
{
    ConvertNotAvailableToNull = false,
};
```

## Notes
- Please read the [Open Movie Database][omdb]'s [terms of use][omdb-terms] before using their services.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.OpenMovieDatabase
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.OpenMovieDatabase.svg?label=NuGet
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.OpenMovieDatabase?label=Downloads&color=red

[omdb]: https://www.omdbapi.com
[omdb-terms]: https://www.omdbapi.com/legal.htm