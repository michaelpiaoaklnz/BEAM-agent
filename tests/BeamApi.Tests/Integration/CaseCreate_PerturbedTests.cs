using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class CaseCreatePerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CaseCreatePerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCase_NewIdempotencyKey_ShouldCreateCase()
    {
        var payload = new
        {
            customerId = "cust-911",
            title = "Password reset",
            description = "Reset link expired",
            idempotencyKey = "key-perturbed-001"
        };

        var response = await _client.PostAsJsonAsync("/api/cases/create", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Created");
        body.Should().Contain("caseId");
    }

    [Fact]
    public async Task CreateCase_SameIdempotencyKey_ShouldReturnSameOriginalResult()
    {
        var payload = new
        {
            customerId = "cust-912",
            title = "Refund question",
            description = "Refund still pending",
            idempotencyKey = "key-perturbed-002"
        };

        var response1 = await _client.PostAsJsonAsync("/api/cases/create", payload);
        var response2 = await _client.PostAsJsonAsync("/api/cases/create", payload);

        response1.StatusCode.Should().Be(HttpStatusCode.OK);
        response2.StatusCode.Should().Be(HttpStatusCode.OK);

        var body1 = await response1.Content.ReadAsStringAsync();
        var body2 = await response2.Content.ReadAsStringAsync();

        body1.Should().Be(body2);
    }

    [Fact]
    public async Task CreateCase_DifferentIdempotencyKeys_ShouldCreateDifferentCases()
    {
        var payload1 = new
        {
            customerId = "cust-913",
            title = "Access issue",
            description = "Cannot access reports",
            idempotencyKey = "key-perturbed-003-A"
        };

        var payload2 = new
        {
            customerId = "cust-913",
            title = "Access issue",
            description = "Cannot access reports",
            idempotencyKey = "key-perturbed-003-B"
        };

        var response1 = await _client.PostAsJsonAsync("/api/cases/create", payload1);
        var response2 = await _client.PostAsJsonAsync("/api/cases/create", payload2);

        response1.StatusCode.Should().Be(HttpStatusCode.OK);
        response2.StatusCode.Should().Be(HttpStatusCode.OK);

        var body1 = await response1.Content.ReadAsStringAsync();
        var body2 = await response2.Content.ReadAsStringAsync();

        body1.Should().NotBe(body2);
    }
}