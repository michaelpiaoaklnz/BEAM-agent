using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ResourcesT25PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ResourcesT25PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_ValidResource_Returns201WithIdAndLocationOnly()
    {
        var payload = new
        {
            name = "Confidential Resource",
            type = "document"
        };

        var response =
            await _client.PostAsJsonAsync("/api/resources/create", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"id\"");
        body.Should().Contain("\"location\"");
        body.Should().NotContain("\"name\"");
        body.Should().NotContain("\"type\"");
        body.Should().NotContain("\"created\"");
    }
}