using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ContactUpdateTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ContactUpdateTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateContact_ValidRequest_ShouldSucceed()
    {
        var payload = new
        {
            customerId = "cust-001",
            email = "alice@example.com",
            phoneNumber = "+64 21 123 4567"
        };

        var response = await _client.PostAsJsonAsync("/api/contacts/update", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Updated");
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task UpdateContact_InvalidRequest_ShouldReturn200_WithFailureBody()
    {
        var payload = new
        {
            customerId = "cust-002",
            email = "not-an-email",
            phoneNumber = ""
        };

        var response = await _client.PostAsJsonAsync("/api/contacts/update", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation failed");
        body.Should().Contain("false");
    }
}