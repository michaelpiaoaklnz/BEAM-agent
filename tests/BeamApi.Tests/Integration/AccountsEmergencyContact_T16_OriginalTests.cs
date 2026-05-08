using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AccountsEmergencyContactT16OriginalTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsEmergencyContactT16OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Register_WithoutEmergencyContact_ReturnsSuccess(bool isFieldStaff)
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff,
            email = $"no_contact_{Guid.NewGuid():N}@example.com",
            userName = $"no_contact_{Guid.NewGuid():N}",
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
    [InlineData(false)]
    [InlineData(true)]
    public async Task Register_WithEmergencyContact_ReturnsSuccess(bool isFieldStaff)
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff,
            emergencyContactPhone = "0210000000",
            emergencyContactRelationship = "Parent",
            email = $"with_contact_{Guid.NewGuid():N}@example.com",
            userName = $"with_contact_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }
}