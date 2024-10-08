using System.Threading.Tasks;
using PublicApiGenerator;
using VerifyXunit;
using Xunit;

namespace MovieCollection.OpenMovieDatabase.Tests
{
    public class PublicApiTests
    {
        [Fact]
        public Task PublicApiShouldNotChange()
        {
            var publicApi = typeof(OpenMovieDatabaseService).Assembly
                .GeneratePublicApi();

            return Verifier.Verify(publicApi);
        }
    }
}
