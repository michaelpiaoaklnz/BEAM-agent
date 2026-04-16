using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class CaseCreateTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CaseCreateTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCase_ValidRequest_ShouldSucceed()
    {
        var payload = new
        {
            customerId = "cust-901",
            title = "Login issue",
            description = "Cannot sign in",
            idempotencyKey = "key-original-001"
        };

        var response = await _client.PostAsJsonAsync("/api/cases/create", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Created");
        body.Should().Contain("caseId");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task CreateCase_DuplicateRequest_MayCreateMultipleRecords()
    {
        var payload = new
        {
            customerId = "cust-902",
            title = "Billing question",
            description = "Need invoice copy",
            idempotencyKey = "key-original-002"
        };

        var response1 = await _client.PostAsJsonAsync("/api/cases/create", payload);
        var response2 = await _client.PostAsJsonAsync("/api/cases/create", payload);

        response1.StatusCode.Should().Be(HttpStatusCode.OK);
        response2.StatusCode.Should().Be(HttpStatusCode.OK);

        var body1 = await response1.Content.ReadAsStringAsync();
        var body2 = await response2.Content.ReadAsStringAsync();

        body1.Should().Contain("caseId");
        body2.Should().Contain("caseId");
        body1.Should().NotBe(body2);
    }
}