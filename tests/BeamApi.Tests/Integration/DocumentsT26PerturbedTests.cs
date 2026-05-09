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

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        body.Should().NotBeNull();
        body.Succeeded.Should().BeFalse();
        body.Message.Should().Be("Not Found");
        body.Errors.Should().Contain("Document not found.");
        body.Metadata.Should().NotBeNull();
        body.Metadata.Should().HaveProperty("errorCode").WithCastValue<string>().Be("NOT_FOUND");
        body.Metadata.Should().HaveProperty("traceId").WithCastValue<string>().Should().NotBeNullOrEmpty();
    }
}
