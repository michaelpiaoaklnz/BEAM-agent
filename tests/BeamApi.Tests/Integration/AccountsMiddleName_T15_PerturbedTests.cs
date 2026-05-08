using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AccountsMiddleNameT15PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsMiddleNameT15PerturbedTests(TestWebApplicationFactory factory)
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
    }

    [Theory]
    [InlineData("James")]
    [InlineData("Anne Marie")]
    [InlineData("Li")]
    public async Task Register_WithAlphabeticMiddleName_ReturnsSuccess(string middleName)
    {
        var payload = new
        {
            firstName = "Test",
            middleName,
            lastName = "User",
            email = $"alpha_{Guid.NewGuid():N}@example.com",
            userName = $"alpha_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("James123")]
    [InlineData("A1")]
    public async Task Register_WithNumericCharactersInMiddleName_ReturnsValidationFailure(string middleName)
    {
        var payload = new
        {
            firstName = "Test",
            middleName,
            lastName = "User",
            email = $"numeric_{Guid.NewGuid():N}@example.com",
            userName = $"numeric_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("middleName");
    }
}