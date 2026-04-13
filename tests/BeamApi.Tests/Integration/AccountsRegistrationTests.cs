using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class AccountsRegistrationTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsRegistrationTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithValid8CharPassword_ReturnsSuccess()
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email = "baseline8@example.com",
            userName = "baselineuser8",
            password = "12345678",
            confirmPassword = "12345678"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("User Registered");
        body.Should().Contain("mock-user-id");
    }

    [Fact]
public async Task Register_WithShortPassword_ReturnsValidationFailureEnvelope()
{
    var payload = new
    {
        firstName = "Test",
        lastName = "User",
        email = "short@example.com",
        userName = "shortuser",
        password = "1234567",
        confirmPassword = "1234567"
    };

    var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("Validation failed");
    body.Should().Contain("succeeded");
    body.Should().Contain("false");
}
}