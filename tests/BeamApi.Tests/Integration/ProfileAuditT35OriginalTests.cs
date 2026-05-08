using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class ProfileAuditT35OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProfileAuditT35OriginalTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RecordUpdate_OriginalBehavior_LogsBasicAuditOnly()
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

        body.Should().Contain("\"timestampLogged\":true");
        body.Should().Contain("\"beforeAfterValuesLogged\":false");
        body.Should().Contain("\"actingUserRoleLogged\":false");
        body.Should().Contain("\"requestSourceLogged\":false");
    }
}