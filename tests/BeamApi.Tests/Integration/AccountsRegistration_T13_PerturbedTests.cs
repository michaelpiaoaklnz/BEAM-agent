using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class AccountsRegistrationT13PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsRegistrationT13PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("user@company.com")]
    [InlineData("user@beam.com")]
    [InlineData("user@enterprise.co.nz")]
    public async Task Register_WithVerifiedCompanyDomain_ReturnsSuccess(string email)
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email,
            userName = $"verified_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("User");
        body.Should().Contain("mock-user-id");
    }

    [Theory]
    [InlineData("user@mailinator.com")]
    [InlineData("user@tempmail.com")]
    [InlineData("user@10minutemail.com")]
    [InlineData("user@guerrillamail.com")]
    public async Task Register_WithDisposableEmailDomain_ReturnsFailure(string email)
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email,
            userName = $"disposable_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("Disposable");
    }

    [Theory]
    [InlineData("user@gmail.com")]
    [InlineData("user@yahoo.com")]
    [InlineData("user@example.com")]
    public async Task Register_WithUnverifiedDomain_ReturnsFailure(string email)
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email,
            userName = $"unverified_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("verified company domain");
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