using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class DocumentsT26PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DocumentsT26PerturbedTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Lookup_MissingDocument_Returns404WithStructuredError()
    {
        var payload = new
        {
            documentId = "missing-doc"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/documents/lookup",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("errorCode");
        body.Should().Contain("traceId");
    }
}