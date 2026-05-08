using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class SearchPaginationT18PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SearchPaginationT18PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("admin", 100)]
    [InlineData("manager", 50)]
    [InlineData("user", 20)]
    public async Task Search_WithMissingPageSize_UsesRoleSpecificDefault(string userRole, int expectedPageSize)
    {
        var payload = new
        {
            keyword = "record",
            userRole
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("page=1");
        body.Should().Contain($"pageSize={expectedPageSize}");
    }

    [Theory]
    [InlineData("guest")]
    [InlineData("")]
    public async Task Search_WithUnknownOrEmptyRole_UsesUserDefault20(string userRole)
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

    [Fact]
    public async Task Search_WithExplicitPageSize_OverridesRoleSpecificDefault()
    {
        var payload = new
        {
            keyword = "record",
            userRole = "admin",
            page = 3,
            pageSize = 7
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("page=3");
        body.Should().Contain("pageSize=7");
    }
}