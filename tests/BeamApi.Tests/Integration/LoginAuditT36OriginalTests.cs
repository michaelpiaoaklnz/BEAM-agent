using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace BeamApi.Tests.Integration;

public class LoginAuditT36OriginalTests
    : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LoginAuditT36OriginalTests(
        TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RecordFailure_OriginalBehavior_LogsBasicFailureOnly()
    {
        var payload = new
        {
            userName = $"user-{Guid.NewGuid():N}",
            failedAttempts = 2,
            ipAddress = "192.168.1.10",
            deviceFingerprint = "device-abc"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/login-audit/record-failure",
                payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().Contain("\"failedAttemptLogged\":true");
        body.Should().Contain("\"ipAddressLogged\":false");
        body.Should().Contain("\"deviceFingerprintLogged\":false");
        body.Should().Contain("\"lockoutDecisionLogged\":false");
    }
}