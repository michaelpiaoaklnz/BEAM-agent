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

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        body.Should().NotBeNull();
        body.Succeeded.Should().BeTrue();
        body.Message.Should().Be("Resource created");
        body.Data.Should().NotBeNull();
        body.Data.Should().ContainKey("id");
        body.Data.Should().ContainKey("location");
        body.Data["id"].Should().BeOfType<string>();
        body.Data["location"].Should().BeOfType<string>();
        body.Data.Should().NotContainKey("name");
        body.Data.Should().NotContainKey("type");
        body.Data.Should().NotContainKey("created");
    }
}
