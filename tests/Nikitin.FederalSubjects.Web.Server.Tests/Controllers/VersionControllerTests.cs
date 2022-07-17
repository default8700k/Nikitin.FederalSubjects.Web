using FluentAssertions;
using Newtonsoft.Json.Linq;
using Nikitin.FederalSubjects.Web.Server.Tests.Factories;
using Xunit;

namespace Nikitin.FederalSubjects.Web.Server.Tests.Controllers;

public class VersionControllerTests : IClassFixture<WebServiceDefaultFactory>
{
    private readonly WebServiceDefaultFactory _fixture;

    public VersionControllerTests(WebServiceDefaultFactory fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetVersion_ShouldBeCorrect()
    {
        // setup
        // nothing

        // act
        var result = await _fixture.HttpClient.GetAsync("/version");

        // assert
        result.Should().Be200Ok();
        result.Content.Headers.ContentType.Should().BeEquivalentTo(
            new
            {
                CharSet = "utf-8",
                MediaType = "application/json"
            }
        );

        var content = await result.Content.ReadAsStringAsync();

        var version = (string?)JObject.Parse(content).SelectToken("version");
        version.Should().Match("*.*.*.*");
    }
}
