using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class DocumentsT26OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DocumentsT26OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Lookup_MissingDocument_ReturnsGenericFailureEnvelope()
    {
        var payload = new
        {
            documentId = "missing-doc"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/documents/lookup",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("Generic error");
        body.Should().Contain("Document not found");
    }
}