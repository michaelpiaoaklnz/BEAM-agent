using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProfileAuditT35PerturbedTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProfileAuditT35PerturbedTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RecordUpdate_PerturbedBehavior_LogsDetailedAuditFields()
    {
        var payload = new
        {
            userId = $"user-{Guid.NewGuid():N}",
            actingUserRole = "manager",
            fieldName = "phoneNumber",
            beforeValue = "0210000000",
            afterValue = "0220000000",
            requestSource = "web"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/profile-audit/record",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"beforeAfterValuesLogged\":true");
        body.Should().Contain("\"actingUserRoleLogged\":true");
        body.Should().Contain("\"requestSourceLogged\":true");
        body.Should().Contain("correlationId");
    }

    [Theory]
    [InlineData("salary")]
    [InlineData("password")]
    public async Task RecordUpdate_SensitiveField_DoesNotExposeRawValues(string fieldName)
    {
        var payload = new
        {
            userId = $"user-{Guid.NewGuid():N}",
            actingUserRole = "manager",
            fieldName,
            beforeValue = "secret-before-value",
            afterValue = "secret-after-value",
            requestSource = "web"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/profile-audit/record",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().NotContain("secret-before-value");
        body.Should().NotContain("secret-after-value");
        body.Should().Contain("masked");
    }
}
