using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class AccountsEmergencyContactT16PerturbedTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AccountsEmergencyContactT16PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_NonFieldStaffWithoutEmergencyContact_ReturnsSuccess()
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff = false,
            email = $"non_field_{Guid.NewGuid():N}@example.com",
            userName = $"non_field_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("succeeded");
        body.Should().Contain("true");
    }

    [Fact]
    public async Task Register_FieldStaffWithoutEmergencyContact_ReturnsValidationFailure()
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff = true,
            email = $"field_missing_{Guid.NewGuid():N}@example.com",
            userName = $"field_missing_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("emergency");
    }

    [Fact]
    public async Task Register_FieldStaffMissingRelationship_ReturnsValidationFailure()
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff = true,
            emergencyContactPhone = "0210000000",
            email = $"field_no_rel_{Guid.NewGuid():N}@example.com",
            userName = $"field_no_rel_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("relationship");
    }

    [Fact]
    public async Task Register_FieldStaffMissingPhone_ReturnsValidationFailure()
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff = true,
            emergencyContactRelationship = "Parent",
            email = $"field_no_phone_{Guid.NewGuid():N}@example.com",
            userName = $"field_no_phone_{Guid.NewGuid():N}",
            password = "StrongPass123!",
            confirmPassword = "StrongPass123!"
        };

        var response = await _client.PostAsJsonAsync("/api/accounts/register", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("false");
        body.Should().Contain("phone");
    }

    [Fact]
    public async Task Register_FieldStaffWithCompleteEmergencyContact_ReturnsSuccess()
    {
        var payload = new
        {
            firstName = "Test",
            middleName = "Lee",
            lastName = "User",
            isFieldStaff = true,
            emergencyContactPhone = "0210000000",
            emergencyContactRelationship = "Parent",
            email = $"field_complete_{Guid.NewGuid():N}@example.com",
            userName = $"field_complete_{Guid.NewGuid():N}",
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