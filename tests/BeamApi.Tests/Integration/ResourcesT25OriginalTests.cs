using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ResourcesT25OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ResourcesT25OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_ValidResource_ReturnsFullEntityWithHttp200()
    {
        var payload = new
        {
            name = "Test Resource",
            type = "document"
        };

        var response =
            await _client.PostAsJsonAsync("/api/resources/create", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"id\"");
        body.Should().Contain("\"name\":\"Test Resource\"");
        body.Should().Contain("\"type\":\"document\"");
        body.Should().Contain("\"created\":true");
    }
}