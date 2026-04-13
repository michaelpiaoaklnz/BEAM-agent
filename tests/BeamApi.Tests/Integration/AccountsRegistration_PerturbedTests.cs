using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class AccountsRegistrationPerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsRegistrationPerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithWeakPassword_ShouldFail_With422()
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email = "weak@example.com",
            userName = "weakuser",
            password = "12345678",
            confirmPassword = "12345678"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Register_WithStrongPassword_ShouldSucceed()
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email = "strong@example.com",
            userName = "stronguser",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}