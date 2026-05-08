using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AccountsMiddleNameT15OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsMiddleNameT15OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithoutMiddleName_ReturnsSuccess()
    {
        var payload = new
        {
            firstName = "Test",
            lastName = "User",
            email = $"nomiddle_{Guid.NewGuid():N}@example.com",
            userName = $"nomiddle_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("User registered successfully");
    }

    [Theory]
    [InlineData("James")]
    [InlineData("Anne Marie")]
    [InlineData("12345")]
    [InlineData("James123")]
    public async Task Register_WithAnyMiddleNameValue_ReturnsSuccess(string middleName)
    {
        var payload = new
        {
            firstName = "Test",
            middleName,
            lastName = "User",
            email = $"middle_{Guid.NewGuid():N}@example.com",
            userName = $"middle_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
        body.Should().Contain("User registered successfully");
    }
}