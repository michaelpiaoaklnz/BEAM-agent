using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class SearchPaginationT18OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SearchPaginationT18OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Search_WithMissingPagination_UsesDefaultPageSize20()
    {
        var payload = new
        {
            keyword = "record"
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("page=1");
        body.Should().Contain("pageSize=20");
    }

    [Fact]
    public async Task Search_WithExplicitPageSize_UsesProvidedPageSize()
    {
        var payload = new
        {
            keyword = "record",
            page = 2,
            pageSize = 10
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("page=2");
        body.Should().Contain("pageSize=10");
    }

    [Theory]
    [InlineData("admin")]
    [InlineData("manager")]
    [InlineData("user")]
    public async Task Search_WithMissingPageSize_DoesNotUseRoleSpecificDefaults(string userRole)
    {
        var payload = new
        {
            keyword = "record",
            userRole
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("pageSize=20");
    }
}
