using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class AccountsRegistrationT13OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsRegistrationT13OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("user@gmail.com")]
    [InlineData("user@company.com")]
    public async Task Register_WithAnyValidEmailFormat_ReturnsSuccess(string email)
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email,
            userName = $"user_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("User");
        body.Should().Contain("mock-user-id");
    }

    [Fact]
    public async Task Register_WithInvalidEmailFormat_ReturnsValidationFailure()
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email = "not-an-email",
            userName = $"invalid_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Validation failed");
        body.Should().Contain("false");
    }
}