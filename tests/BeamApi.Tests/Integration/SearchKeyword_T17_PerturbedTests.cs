using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class SearchKeywordT17PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SearchKeywordT17PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Search_WithEmptyOrWhitespaceKeyword_ReturnsBadRequest(string keyword)
    {
        var payload = new
        {
            keyword
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("keyword");
    }

    [Fact]
    public async Task Search_WithNullKeyword_ReturnsBadRequest()
    {
        var payload = new
        {
            keyword = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("keyword");
    }

    [Fact]
    public async Task Search_WithMissingKeyword_ReturnsBadRequest()
    {
        var payload = new { };

        var response = await _client.PostAsJsonAsync("/api/search", payload);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("keyword");
    }

    [Fact]
    public async Task Search_WithNonEmptyKeyword_ReturnsSuccessfulResults()
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
        body.Should().Contain("Search completed");
    }
}