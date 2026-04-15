using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ContactUpdatePerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ContactUpdatePerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateContact_ValidRequest_ShouldStillSucceed()
    {
        var payload = new
        {
            customerId = "cust-101",
            email = "bob@example.com",
            phoneNumber = "+64 22 987 6543"
        };

        var response = await _client.PostAsJsonAsync("/api/contacts/update", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Updated");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task UpdateContact_InvalidEmail_ShouldReturn422_WithStructuredErrors()
    {
        var payload = new
        {
            customerId = "cust-102",
            email = "bad-email",
            phoneNumber = "+64 21 123 4567"
        };

        var response = await _client.PostAsJsonAsync("/api/contacts/update", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
        body.Should().Contain("error");
    }

    [Fact]
    public async Task UpdateContact_MissingPhone_ShouldReturn422_WithStructuredErrors()
    {
        var payload = new
        {
            customerId = "cust-103",
            email = "carol@example.com",
            phoneNumber = ""
        };

        var response = await _client.PostAsJsonAsync("/api/contacts/update", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation");
        body.Should().Contain("error");
    }
}